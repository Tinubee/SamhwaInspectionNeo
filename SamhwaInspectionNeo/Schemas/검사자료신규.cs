using ClosedXML.Excel;
using DevExpress.Office.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SamhwaInspectionNeo;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IVM.Schemas
{
    public class 검사자료신규
    {
        private readonly 검사조회서비스 _조회;
        private readonly 검사정리서비스 _정리;
        private readonly 검사저장서비스 _저장;
        private readonly 검사계산서비스 _계산;
        private readonly 검사가공서비스 _가공;

        public delegate void 검사완료알림핸들러(검사결과 결과);
        public delegate void 수동검사알림핸들러(카메라구분 카메라, 검사결과 결과);
        public event 검사완료알림핸들러 검사완료알림;
        public event 수동검사알림핸들러 수동검사알림;

        [JsonIgnore]
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Inspection", "검사자료");
        [JsonIgnore]
        private Dictionary<Int32, 검사결과> 검사스플 = new Dictionary<Int32, 검사결과>();
        [JsonIgnore]
        public 검사결과 수동검사;

        public 검사자료신규(string connStr)
        {
            _조회 = new 검사조회서비스(connStr);
            _정리 = new 검사정리서비스(connStr);
            _저장 = new 검사저장서비스(connStr);
            _계산 = new 검사계산서비스(connStr);
            _가공 = new 검사가공서비스(connStr);
        }

        public void Init()
        {

            //현 상태 한 모델과 결합되어 있음.
            //스크래치 덴트 완료 후 리모델링 예정.
            //수동검사초기화();
        }

        public void Add(검사결과 결과)
        {
            try
            {
                _저장.Add(결과);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Data Add Error", ex.Message, true);
            }
        }

        public void Notify(검사결과 결과)
        {
            try
            {
                검사완료알림?.Invoke(결과);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Data Add Error", ex.Message, true);
            }
        }

        public void AddAndNotify(검사결과 결과)
        {
            try
            {
                _저장.Add(결과);
                검사완료알림?.Invoke(결과);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Data Add Error", ex.Message, true);
            }
        }

        public void Remove(검사결과 결과)
        {
            try
            {
                _정리.Remove(결과);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Data Remove Error", ex.Message, true);
            }
        }

        public void RemoveAndNotify(검사결과 결과)
        {
            try
            {
                _정리.Remove(결과);
                //업데이트완료알림?.Invoke(결과);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Data Remove Error", ex.Message, true);
            }
        }

        public IQueryable<검사결과> GetQueryable(DateTime 시작, DateTime 종료) => _조회.GetQueryable(시작, 종료);

        public List<검사결과> GetResultList(DateTime 시작, DateTime 종료) => _조회.GetResultList(시작, 종료);

        public List<검사정보> GetDetailsByDate(DateTime 일시) => _조회.GetDetailsByDate(일시);

        public 검사결과 GetLatestResultWithDetail(int days) => _조회.GetLatestResultWithDetail(days);

        public List<검사결과> GetResultListWithDetail(DateTime 시작, DateTime 종료) => _조회.GetResultListWithDetail(시작, 종료);

        public void Close()
        {
            try
            {
                //_정리.자료정리(Global.환경설정.결과보관);
                _저장.Close();
            }
            catch (Exception ex)
            {
                Global.오류로그("검사", "정리 실패", ex.Message, true);
            }
        }

        #region 리팩토링 필요 영역(기존 함수들)
        // 기존 함수 추가(추후 리팩토링 필요 영역)
        private void 수동검사초기화()
        {
            this.수동검사 = new 검사결과();
            this.수동검사.검사코드 = 9999;
            this.수동검사.Reset();
        }

        // 현재 검사중인 정보를 검색
        public 검사결과 검사항목찾기(Int32 검사코드, Boolean 신규여부 = false)
        {
            if (!Global.장치상태.자동수동) return this.수동검사;
            검사결과 검사 = null;
            if (검사코드 > 0 && this.검사스플.ContainsKey(검사코드))
                검사 = this.검사스플[검사코드];
            if (검사 == null && !신규여부)
                Global.오류로그(로그영역.GetString(), "검사항목찾기", $"[{검사코드}] Index가 없습니다.", true);
            return 검사;
        }

        public void 검사시작(Int32 검사코드, DateTime 검사일시)
        {
            if (!Global.장치상태.자동수동)
            {
                this.수동검사.Reset();
                return;
            }

            검사결과 검사 = 검사항목찾기(검사코드, true);
            if (검사 == null)
            {
                검사 = new 검사결과() { 검사코드 = 검사코드 };
                검사.Reset();
                this.검사스플.Add(검사.검사코드, 검사);
            }
        }

        public void ExportToExcel(List<List<string>> InputResult, bool IsTranspose, string filePath) => _가공.ExportToExcel(InputResult, IsTranspose, filePath);

        public void ExportToCsv(List<List<string>> InputResult, string filePath) => _가공.ExportToCsv(InputResult, filePath);

        public void 수동검사결과(카메라구분 카메라, 검사결과 검사) => this.수동검사알림?.Invoke(카메라, 검사);

        public 검사결과 현재검사찾기()
        {
            if (!Global.장치상태.자동수동) return this.수동검사;
            if (this.검사스플.Count < 1) return this.수동검사;
            return this.검사스플.Last().Value;
        }

        public void 검사종료(int 검사코드) => this.검사스플.Remove(검사코드);

        #endregion
    }

    public class 검사테이블신규 : DbContext
    {
        public DbSet<검사결과> 검사결과 => Set<검사결과>();
        public DbSet<검사정보> 검사정보 => Set<검사정보>();

        public 검사테이블신규(DbContextOptions<검사테이블신규> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<검사결과>().HasKey(x => new { x.검사일시, x.검사코드 });
            modelBuilder.Entity<검사결과>().Property(e => e.모델구분).HasConversion(new EnumToNumberConverter<모델구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.CTQ결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.외관결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());

            modelBuilder.Entity<검사정보>().HasKey(x => new { x.검사일시, x.검사항목 });
            modelBuilder.Entity<검사정보>().Property(e => e.검사그룹).HasConversion(new EnumToNumberConverter<검사그룹, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.검사항목).HasConversion(new EnumToNumberConverter<검사항목, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.검사장치).HasConversion(new EnumToNumberConverter<장치구분, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.결과분류).HasConversion(new EnumToNumberConverter<결과분류, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.측정단위).HasConversion(new EnumToNumberConverter<단위구분, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
        }
    }

    public class 검사저장서비스
    {
        private readonly BlockingCollection<검사결과> _Queue = new BlockingCollection<검사결과>();
        private readonly DbContextOptions<검사테이블신규> _options;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public 검사저장서비스(string connStr)
        {
            _options = new DbContextOptionsBuilder<검사테이블신규>()
                .UseNpgsql(connStr)
                .Options;

            Task.Run(() => 처리스레드(_cts.Token));
        }

        public void Add(검사결과 결과) => _Queue.Add(결과);

        private async Task 처리스레드(CancellationToken token)
        {
            foreach (var 결과 in _Queue.GetConsumingEnumerable(token))
            {
                try
                {
                    using (var db = new 검사테이블신규(_options))
                    {
                        db.검사결과.Add(결과);
                        db.검사정보.AddRange(결과.검사내역);
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[검사저장오류] {ex.Message}");
                }
            }
        }

        public void Close()
        {
            _Queue.CompleteAdding();
            _cts.Cancel();
        }
    }

    public class 검사조회서비스
    {
        private readonly DbContextOptions<검사테이블신규> _options;

        public 검사조회서비스(string connectionString)
        {
            _options = new DbContextOptionsBuilder<검사테이블신규>()
                .UseNpgsql(connectionString)
                .Options;
        }

        // ✅ ServerModeSource 연결용 Queryable
        // LogViewer에서 실시간 스크롤할 때마다 자동 로딩용
        public IQueryable<검사결과> GetQueryable(DateTime 시작, DateTime 종료)
        {
            var context = new 검사테이블신규(_options);
            return context.검사결과
                .Where(x => x.검사일시 >= 시작 && x.검사일시 < 종료.AddDays(1));
        }

        // 필터 맞는 검사결과들(검사결과만)
        public List<검사결과> GetResultList(DateTime 시작, DateTime 종료, 모델구분 모델 = 모델구분.Model_2PB, int 코드 = 0, string serial = null)
        {
            using (var db = new 검사테이블신규(_options))
            {
                // 최신검사결과 Select
                return db.검사결과
                    .Where(l => l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1))
                    .Where(l => 코드 <= 50 || l.검사코드 == 코드)
                    .OrderByDescending(l => l.검사일시)
                    .AsNoTracking()
                    .ToList();
            }
        }

        // 필터 맞는 검사결과들(검사내역포함)
        public List<검사결과> GetResultListWithDetail(DateTime 시작, DateTime 종료, 모델구분 모델 = 모델구분.None, int 코드 = 0, string serial = null)
        {
            using (var db = new 검사테이블신규(_options))
            {
                var 결과 = db.검사결과
                    .Where(l => l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1))
                    .Where(l => 코드 <= 50 || l.검사코드 == 코드)
                    .OrderByDescending(l => l.검사일시)
                    .AsNoTracking()
                    .ToList();

                var 검사일시리스트 = 결과.Select(r => r.검사일시).ToList();

                var allDetails = db.검사정보
                    .Where(d => d.검사일시 >= 시작 && d.검사일시 < 종료.AddDays(1))
                    .OrderBy(d => d.검사항목)
                    .AsNoTracking()
                    .ToList();

                var groupedDetails = allDetails
                    .GroupBy(d => d.검사일시)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var r in 결과)
                {
                    if (groupedDetails.TryGetValue(r.검사일시, out var details))
                        r.검사내역.AddRange(details);
                }

                return 결과;
            }




            //List<검사결과> 결과 = GetResultList(시작, 종료, 모델, 코드, serial);

            //결과.ForEach(x => x.검사내역.AddRange(GetDetailsByDate(x.검사일시)));

            //return 결과;
        }

        // 가장 마지막 검사결과 Select
        public 검사결과 GetLatestResult(DateTime 시작, DateTime 종료, 모델구분 모델 = 모델구분.None, int 코드 = 0, string serial = null)
        {
            using (var db = new 검사테이블신규(_options))
            {
                // 최신검사결과 Select
                return db.검사결과
                    .Where(l => l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1))
                    .Where(l => 코드 <= 0 || l.검사코드 == 코드)
                    .Where(l => 모델 == 모델구분.None || l.모델구분 == 모델)
                    .OrderByDescending(l => l.검사일시)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        //일수 입력하면 오늘 기점으로 해당 일수 이전의 검사결과를 조회
        public 검사결과 GetLatestResultWithDetail(int days)
        {
            검사결과 결과 = GetLatestResult(DateTime.Today.AddDays(-days), DateTime.Today);

            List<검사정보> 결과세부내역 = GetDetailsByDate(결과.검사일시);

            결과.검사내역.AddRange(결과세부내역);

            return 결과;
        }

        // 검사일시로 검사정보리스트(세부사항) 불러옴
        public List<검사정보> GetDetailsByDate(DateTime 검사일시)
        {
            using (var db = new 검사테이블신규(_options))
            {
                return db.검사정보
                         .Where(x => x.검사일시 == 검사일시)
                         .OrderBy(x => x.검사항목)
                         .AsNoTracking()
                         .ToList();
            }
        }
    }

    public class 검사정리서비스
    {
        private readonly DbContextOptions<검사테이블신규> _options;

        public 검사정리서비스(string connectionString)
        {
            _options = new DbContextOptionsBuilder<검사테이블신규>()
                .UseNpgsql(connectionString)
                .Options;
        }

        public void Remove(검사결과 결과)
        {
            using (var db = new 검사테이블신규(_options))
            {
                db.검사결과.Remove(결과);
                db.검사정보.RemoveRange(결과.검사내역);
                db.SaveChanges();
            }
        }

    }

    public class 검사가공서비스
    {
        private readonly DbContextOptions _options;

        public 검사가공서비스(string connectionString)
        {
            _options = new DbContextOptionsBuilder<검사테이블신규>()
                .UseNpgsql(connectionString)
                .Options;
        }

        // StartRow : Excel index에 표시된 시작행 번호
        // EndRow   : Excel index에 표시된 종료행 번호
        public Dictionary<string, decimal> CmmValueExtractor(string filePath, int startRow, int EndRow)
        {
            var dataDictionary = new Dictionary<string, decimal>();
            try
            {
                if (!File.Exists(filePath))
                {
                    Global.오류로그("Calibration", "Error", "CMM File is not found.", true);
                    return null;
                }

                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(file); // .xlsx 파일 읽기
                    ISheet sheet = workbook.GetSheetAt(0); // 첫 번째 시트 선택

                    if (sheet == null)
                    {
                        Global.오류로그("Calibration", "Error", "Worksheet could not be found.", true);
                        return null;
                    }

                    // A열 (0부터 시작, A열은 0번째 인덱스)과 B열 (1번째 인덱스)
                    int keyColumnIndex = 0; // A열
                    int valueColumnIndex = 1; // B열

                    //(NPOI는 0부터 시작하므로 시작 인덱스는 -1)
                    for (int rowIndex = startRow - 1; rowIndex < EndRow; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row != null)
                        {
                            ICell keyCell = row.GetCell(keyColumnIndex);
                            ICell valueCell = row.GetCell(valueColumnIndex);

                            if (keyCell != null && keyCell.CellType == CellType.String &&
                                valueCell != null && valueCell.CellType == CellType.Numeric)
                            {
                                string key = keyCell.StringCellValue;
                                decimal value = Convert.ToDecimal(valueCell.NumericCellValue);

                                if (!dataDictionary.ContainsKey(key))
                                {
                                    dataDictionary.Add(key, value);
                                }
                                else
                                {
                                    Global.오류로그("Calibration", "Warning", $"Duplicate key '{key}' found at row {rowIndex + 1}.", true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.오류로그("Calibration", "Error", $"Reading the Excel file is failed : {ex}", true);
                return null;
            }
            return dataDictionary;
        }
        //public void ExportToExcel(List<List<string>> InputResult, bool IsTranspose, string filePath)
        //{
        //    string directoryPath = Path.GetDirectoryName(filePath);
        //    if (!string.IsNullOrEmpty(directoryPath))
        //        Directory.CreateDirectory(directoryPath);

        //    List<List<string>> result = IsTranspose ? TransposeList(InputResult) : InputResult;

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("검사 결과");

        //        // ✅ DataTable로 변환
        //        var dt = new DataTable();
        //        int colCount = result[0].Count;
        //        for (int c = 0; c < colCount; c++)
        //            dt.Columns.Add($"{c + 1}");

        //        for (int r = 1; r < result.Count; r++) // 첫 행은 헤더로 쓸 수도 있음
        //            dt.Rows.Add(result[r].ToArray());

        //        // ✅ 한 번에 로드 (매우 빠름)
        //        worksheet.Cell(1, 1).InsertTable(dt, false);

        //        workbook.SaveAs(filePath);
        //    }
        //}

        public void ExportToCsv(List<List<string>> result, string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var row in result)
                    writer.WriteLine(string.Join(",", row.Select(v => v.Replace(",", " "))));
            }
        }
        public void ExportToExcel(List<List<string>> InputResult, bool IsTranspose, string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);

            //폴더가 존재하지 않으면 생성
            if (!string.IsNullOrEmpty(directoryPath)) Directory.CreateDirectory(directoryPath);

            // 행과 열을 전치하여 새로운 데이터 구조 생성
            List<List<string>> result = InputResult;
            if (IsTranspose) result = TransposeList(result);

            // 데이터리스트 엑셀에 저장로직
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("검사 결과");

                for (int rowIndex = 0; rowIndex < result.Count; rowIndex++)
                {
                    List<string> rowData = result[rowIndex];

                    for (int colIndex = 0; colIndex < rowData.Count; colIndex++)
                    {
                        // 🔹 [1] 현재 셀의 값
                        string cellValue = rowData[colIndex];

                        // 🔹 [2] 숫자 변환 시도
                        if (decimal.TryParse(cellValue, out decimal numericValue))
                        {
                            // 숫자로 변환되면 숫자로 저장
                            worksheet.Cell(rowIndex + 1, colIndex + 1).Value = numericValue;
                        }
                        else
                        {
                            // 변환이 안 되면 문자열로 저장
                            worksheet.Cell(rowIndex + 1, colIndex + 1).Value = cellValue;
                        }
                    }
                }
                workbook.SaveAs(filePath);
            }
        }

        public List<List<string>> TransposeList(List<List<string>> originalList)
        {
            if (originalList == null || originalList.Count == 0)
                return new List<List<string>>();

            // 열(Column) 개수만큼 새로운 리스트 생성
            int columnCount = originalList.Max(row => row.Count);
            List<List<string>> transposed = new List<List<string>>();

            for (int i = 0; i < columnCount; i++)
            {
                transposed.Add(new List<string>());
            }

            // 데이터를 전치 (행 -> 열, 열 -> 행)
            foreach (var row in originalList)
            {
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    if (colIndex < row.Count)
                        transposed[colIndex].Add(row[colIndex]);
                    else
                        transposed[colIndex].Add(string.Empty); // 빈 값 채우기
                }
            }
            return transposed;
        }
    }

    public class 검사계산서비스
    {
        private readonly DbContextOptions<검사테이블신규> _options;

        public 검사계산서비스(string connectionString)
        {
            _options = new DbContextOptionsBuilder<검사테이블신규>()
                .UseNpgsql(connectionString)
                .Options;
        }

    }
}
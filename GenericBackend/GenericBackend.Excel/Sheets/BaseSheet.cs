using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.Excel.Structures;

namespace GenericBackend.Excel.Sheets
{
    //Strategy pattern
    public abstract class BaseSheet
    {
        private readonly Sheet _sheet;
        private readonly WorksheetPart _worksheetPart;

        protected readonly WorkbookPart WorkbookPart;

        protected BaseSheet(Sheet sheet, WorkbookPart workbookPart, WorksheetPart worksheetPart)
        {
            _sheet = sheet;
            WorkbookPart = workbookPart;
            _worksheetPart = worksheetPart;
        }

        protected MongoSheetData GetStructure(string name, IEnumerable<Row> rows, ICollection<int> years,
            ICollection<int> monthes)
        {
            return new MongoSheetData
            {
                Name = name.ToLowerInvariant(),
                Years = years,
                Monthes = monthes,
                Elements = GetElements(rows.ToArray())
            };
        }

        protected abstract IEnumerable<SheetItem> GetElements(ICollection<Row> rows);

        protected abstract int ColumnStartNumber { get; }

        protected abstract ICollection<int> ParseYears(IEnumerable<Row> rows);

        protected abstract ICollection<int> ParseMonthes(IEnumerable<Row> rows);

        protected static IEnumerable<Cell> GetCellsFrom(IEnumerable<Row> rows, int startIndex, int rowIndex)
        {
            return GetCellFromRow(rows.Skip(rowIndex).First(), startIndex).ToArray();
        }

        protected static IEnumerable<Cell> GetCellFromRow(OpenXmlElement row, int startIndex)
        {
            return row.Descendants<Cell>().Skip(startIndex);
        }

        public MongoSheetData Parse()
        {

            var name = _sheet.Name.Value.ToLower();
            
            var sheetData = _worksheetPart.Worksheet.ChildElements.First<SheetData>();

            var rows = sheetData.Elements<Row>().ToArray();

            var years = ParseYears(rows);
            var monthes = ParseMonthes(rows);

            return GetStructure(name, rows, years, monthes);    
        }
    }
}

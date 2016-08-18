using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.DataModels.Actual;

namespace GenericBackend.Excel.Sheets
{
    //Strategy pattern
    public abstract class BaseSheet<T>
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

        protected abstract T GetStructure(string name, IEnumerable<Row> rows, ICollection<int> years, ICollection<int> monthes);
        
        protected abstract int ColumnStartNumber { get; }

        protected abstract ICollection<int> ParseYears();

        protected abstract ICollection<int> ParseMonthes(); 

        public T Parse()
        {

            var name = _sheet.Name.Value.ToLower();
            
            var sheetData = _worksheetPart.Worksheet.ChildElements.First<SheetData>();

            var rows = sheetData.Elements<Row>().ToArray();

            var years = ParseYears();
            var monthes = ParseYears();

            return GetStructure(name, rows, years, monthes);    
        }
    }
}

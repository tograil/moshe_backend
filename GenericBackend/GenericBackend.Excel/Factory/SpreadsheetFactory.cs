using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.Excel.Sheets;

namespace GenericBackend.Excel.Factory
{
    public class SheetFactory : IDisposable
    {
        private readonly string _docPath;
        private readonly SpreadsheetDocument _doc = null;

        public SheetFactory(string path)
        {
            _docPath = path;
        }

        private SpreadsheetDocument GetDocument()
        {
            return _doc ?? SpreadsheetDocument.Open(_docPath, true);
        }

        public T GetSheet<T>(string sheetName, Func<Sheet, WorkbookPart, WorksheetPart, T> createSheet)
        {
            var document = GetDocument();
            
            var sheet = (Sheet)document.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>()
                    .ChildElements.First(x => x is Sheet && ((Sheet)x).Name.Value.Equals(sheetName, StringComparison.CurrentCultureIgnoreCase));

            var workSheetPart =
                (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);

            return createSheet(sheet, document.WorkbookPart, workSheetPart);
            
        }


        public void Dispose()
        {
            _doc?.Dispose();
        }
    }
}

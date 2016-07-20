using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace GenericBackend.Excel
{
    public class ParsePlanActual
    {
        public static void Parse(string docName)
        {
            using (var document = SpreadsheetDocument.Open(docName, true))
            {
                var sheet =
                    (Sheet)document.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                        .ChildElements.First(x => x is Sheet && ((Sheet)x).Name.Value.Equals("Actual", StringComparison.CurrentCultureIgnoreCase));




                var workSheetPart =
                    (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);


                var stringParts = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>();

                var sheetData = workSheetPart.Worksheet.ChildElements.First<SheetData>();



                var rows = sheetData.Elements<Row>();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.Core.Extensions;
using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;

namespace GenericBackend.Excel
{
    public class ParsePlanActual
    {
        public const string PlanSheetName = "plan";
        public const string ActualSheetName = "actual";
        private readonly string _docPath;

        public ParsePlanActual(string docPath)
        {
            _docPath = docPath;
        }

        public PlanSheet ParsePlanSheet()
        {
            var planSheet = new PlanSheet();
            using (var document = SpreadsheetDocument.Open(_docPath, true))
            {
                var sheet =
                    (Sheet)document.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                        .ChildElements.First(x => x is Sheet && ((Sheet)x).Name.Value.Equals(PlanSheetName, StringComparison.CurrentCultureIgnoreCase));
                planSheet.Name = sheet.Name.Value.ToLower();
                var planItems = new List<PlanSheetItem>();
                
                var workSheetPart =
                    (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);

                var sheetData = workSheetPart.Worksheet.ChildElements.First<SheetData>();
                
                var rows = sheetData.Elements<Row>().ToArray();

                var years = ParseYears(rows[0].Descendants<Cell>().ToArray(), document, 17);
                var monthes = ParseMonthes(rows[1].Descendants<Cell>().ToArray(), document, 17);
                
                foreach (var row in rows.Skip(3))
                {
                    var planItem = GetDataRow(row.Descendants<Cell>().ToArray(), document, years, monthes);
                    planItems.Add(planItem);
                    planSheet.PlanItems = planItems;
                }
            }
            
            return planSheet;
        }

        public ActualSheet ParseActualSheet()
        {
            var planSheet = new ActualSheet();
            using (var document = SpreadsheetDocument.Open(_docPath, true))
            {
                var sheet =
                    (Sheet)document.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                        .ChildElements.First(x => x is Sheet && ((Sheet)x).Name.Value.Equals(ActualSheetName, StringComparison.CurrentCultureIgnoreCase));
                planSheet.Name = sheet.Name.Value.ToLower();
                var planItems = new List<ActualSheetItem>();

                var workSheetPart =
                    (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);

                var sheetData = workSheetPart.Worksheet.ChildElements.First<SheetData>();

                var rows = sheetData.Elements<Row>().ToArray();

                var years = ParseActualYears(rows[0].Descendants<Cell>().ToArray(), document, 16);
                var monthes = ParseMonthes(rows[0].Descendants<Cell>().ToArray(), document, 16);

                foreach (var row in rows.Skip(2))
                {
                    var planItem = GetActualDataRow(row.Descendants<Cell>().ToArray(), document, years, monthes);
                    planItems.Add(planItem);
                    planSheet.ActualItems = planItems;
                }
            }

            return planSheet;
        }


        private static PlanSheetItem GetDataRow(IReadOnlyList<Cell> cells, SpreadsheetDocument document, ICollection<int> years, ICollection<int> monthes)
        {
            var planItem = new PlanSheetItem
            {
                Subject = GetCellValue(document.WorkbookPart, cells[4]),
                TimelineData = GetData(cells, document, 17, years, monthes)
            };

            return planItem;
        }


        private static ActualSheetItem GetActualDataRow(IReadOnlyList<Cell> cells, SpreadsheetDocument document, ICollection<int> years, ICollection<int> monthes)
        {
            var planItem = new ActualSheetItem
            {
                Subject = GetCellValue(document.WorkbookPart, cells[4]),
                TimelineData = GetActualData(cells, document, 16, years, monthes)
            };

            return planItem;
        }

        private static ICollection<PlanTimelineData> GetData(IReadOnlyList<Cell> cells, SpreadsheetDocument document, int startIndex, ICollection<int> years, ICollection<int> monthes)
        {
            var list = new List<PlanTimelineData>();

            for (var j = startIndex; j <= cells.Count - 3; j = j + 3)
            {
                var timeLine = new PlanTimelineData
                {
                    Year = years.ElementAt(j-startIndex),
                    Month = monthes.ElementAt(j-startIndex),
                    Plan = GetCellValue(document.WorkbookPart, cells[j]),
                    AccumulatedPlan = GetCellValue(document.WorkbookPart, cells[j + 1]),
                    SupervisorComments = GetCellValue(document.WorkbookPart, cells[j + 2])
                };

                list.Add(timeLine);
            }

            return list;
        }


        private static ICollection<ActualTimelineData> GetActualData(IReadOnlyList<Cell> cells, SpreadsheetDocument document, int startIndex, ICollection<int> years, ICollection<int> monthes)
        {
            var list = new List<ActualTimelineData>();

            for (var j = startIndex; j <= cells.Count - 5; j = j + 5)
            {
                var timeLine = new ActualTimelineData
                {
                    Year = years.ElementAt(j - startIndex),
                    Month = monthes.ElementAt(j - startIndex),
                    Actual = GetCellValue(document.WorkbookPart, cells[j]),
                    UpdateActual = GetCellValue(document.WorkbookPart, cells[j + 1]),
                    AccumulatedActual = GetCellValue(document.WorkbookPart, cells[j + 2]),
                    AccumulatedUpdate = GetCellValue(document.WorkbookPart, cells[j + 3]),
                    SupervisorComments = GetCellValue(document.WorkbookPart, cells[j + 4])
                };

                list.Add(timeLine);
            }

            return list;
        }

        private static ICollection<int> ParseActualYears(IEnumerable<Cell> cells, SpreadsheetDocument document, int startIndex)
        {
            var cellsData = cells.Skip(startIndex).Select(x => GetCellValue(document.WorkbookPart, x)).ToArray();

            var knownCell = cellsData[0];

            for (var i = 1; i < cellsData.Length; i++)
            {
                if (cellsData[i].IsNullOrEmpty())
                {
                    cellsData[i] = knownCell;
                }
                else
                {
                    knownCell = cellsData[i];
                }
            }

            return cellsData.Select(cell => DateTime.FromOADate(double.Parse(cell)).Year).ToArray();
        }

        private static ICollection<int> ParseMonthes(IEnumerable<Cell> cells, SpreadsheetDocument document, int startIndex)
        {
            var cellsData = cells.Skip(startIndex).Select(x => GetCellValue(document.WorkbookPart, x)).ToArray();

            var knownCell = cellsData[0];

            for (var i = 1; i < cellsData.Length; i++)
            {
                if (cellsData[i].IsNullOrEmpty())
                {
                    cellsData[i] = knownCell;
                }
                else
                {
                    knownCell = cellsData[i];
                }
            }

            return cellsData.Select(cell => DateTime.FromOADate(double.Parse(cell)).Month).ToArray();
        }

        private static ICollection<int> ParseYears(IEnumerable<Cell> cells, SpreadsheetDocument document, int startIndex)
        {
            var cellsData = cells.Skip(startIndex).Select(x => GetCellValue(document.WorkbookPart, x)).ToArray();

            var knownCell = cellsData[0];

            for(var i=1; i<cellsData.Length; i++)
            {
                if (cellsData[i].IsNullOrEmpty())
                {
                    cellsData[i] = knownCell;
                }
                else
                {
                    knownCell = cellsData[i];
                }
            }

            return cellsData.Select(int.Parse).ToArray();
        }

        // Retrieve the value of a cell, given a file name, sheet name,  
        // and address name. 
        public static string GetCellValue(
            string addressName, WorkbookPart wbPart, Sheet theSheet)
        {
            // Throw an exception if there is no sheet. 
            if (theSheet == null)
            {
                throw new ArgumentException("sheetName");
            }

            // Retrieve a reference to the worksheet part. 
            WorksheetPart wsPart =
                (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

            // Use its Worksheet property to get a reference to the cell  
            // whose address matches the address you supplied. 
            Cell theCell = wsPart.Worksheet.
              Descendants<Cell>().FirstOrDefault(c => c.CellReference == addressName);

            // If the cell does not exist, return an empty string. 
            //value = GetCellValue(wbPart, theCell, value);
            return null;
        }

        private static string GetCellValue(WorkbookPart wbPart, Cell theCell)
        {
            string value = string.Empty;
            if (theCell == null)
                return value;

            value = theCell.InnerText;

            // If the cell represents an integer number, you are done.  
            // For dates, this code returns the serialized value that  
            // represents the date. The code handles strings and  
            // Booleans individually. For shared strings, the code  
            // looks up the corresponding value in the shared string  
            // table. For Booleans, the code converts the value into  
            // the words TRUE or FALSE. 
            if (theCell.DataType == null)
                return value;

            switch (theCell.DataType.Value)
            {
                case CellValues.SharedString:

                    // For shared strings, look up the value in the 
                    // shared strings table. 
                    var stringTable =
                        wbPart.GetPartsOfType<SharedStringTablePart>()
                            .FirstOrDefault();

                    // If the shared string table is missing, something  
                    // is wrong. Return the index that is in 
                    // the cell. Otherwise, look up the correct text in  
                    // the table. 
                    if (stringTable != null)
                    {
                        value =
                            stringTable.SharedStringTable
                                .ElementAt(int.Parse(value)).InnerText;
                    }
                    break;

                case CellValues.Boolean:
                    switch (value)
                    {
                        case "0":
                            value = "FALSE";
                            break;
                        default:
                            value = "TRUE";
                            break;
                    }
                    break;
            }
            return value;
        }
    }
}

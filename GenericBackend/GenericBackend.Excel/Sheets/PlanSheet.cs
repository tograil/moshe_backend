using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.Core.Extensions;
using GenericBackend.Excel.Generic;
using GenericBackend.Excel.Structures;

namespace GenericBackend.Excel.Sheets
{
    public class PlanSheet : BaseSheet
    {
        private const int DataStartIndex = 17;
        private const int Step = 3;
        private const int ItemStartIndex = 3;
        private const int TitlesIndex = 2;
        private const int NameIndex = 4;


        public PlanSheet(Sheet sheet, WorkbookPart workbookPart, WorksheetPart worksheetPart)
            : base(sheet, workbookPart, worksheetPart)
        {

        }

        protected override int ColumnStartNumber => DataStartIndex;

        protected override IEnumerable<SheetItem> GetElements(ICollection<Row> rows)
        {
            var nameCells = GetCellsFrom(rows, DataStartIndex, TitlesIndex).Take(Step).Select(x => GeneralParsing.GetCellValue(WorkbookPart, x)).ToArray();

            var elements = new List<SheetItem>();

            foreach (var source in rows.Skip(ItemStartIndex))
            {
                var cells = GetCellFromRow(source, 0).ToArray();

                var name = GeneralParsing.GetCellValue(WorkbookPart, cells.Skip(NameIndex).First());
                var data = nameCells.Select(
                        (x, i) =>
                            new
                            {
                                Name = x,
                                Data = cells.Skip(DataStartIndex).Where((y, j) => j % Step == i).Select(z => GeneralParsing.GetCellValue(WorkbookPart, z)).ToArray()
                                        
                            }).ToDictionary(dataKey => dataKey.Name, dataCheck => dataCheck.Data);


                var item = new SheetItem
                {
                    Name = name,
                    Data = data
                };

                elements.Add(item);

            }

            return elements;
        }

        protected override ICollection<int> ParseMonthes(IEnumerable<Row> rows)
        {
            return GetMonthInts(GetMonthCells(rows)).Select(x => DateTime.FromOADate(x).Month).Where((x, i) => (i % Step) == 0).ToArray();
        }

        protected override ICollection<int> ParseYears(IEnumerable<Row> rows)
        {
            var cellsData =
                GetYearCells(rows)
                    .Skip(DataStartIndex)
                    .Select(x => GeneralParsing.GetCellValue(WorkbookPart, x))
                    .ToArray();

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

            return cellsData.Select(int.Parse).Where((x, i) => (i % Step) == 0).ToArray();
        }

        private static IEnumerable<Cell> GetYearCells(IEnumerable<Row> rows)
        {
            return rows.First().Descendants<Cell>().ToArray();
        }

        private static IEnumerable<Cell> GetMonthCells(IEnumerable<Row> rows)
        {
            return rows.Skip(1).First().Descendants<Cell>().ToArray();
        }

        private IEnumerable<int> GetMonthInts(IEnumerable<Cell> cells)
        {
            var cellsData =
                cells.Skip(DataStartIndex).Select(x => GeneralParsing.GetCellValue(WorkbookPart, x)).ToArray();

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

            return cellsData.Select(int.Parse).ToArray();
        }


    }
}

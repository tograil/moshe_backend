using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.Excel.Sheets;
using GenericBackend.Excel.Structures;

namespace GenericBackend.Excel.Sheets
{
    public class PlanSheet : BaseSheet<PlanSheetData>
    {
        public PlanSheet(Sheet sheet, WorkbookPart workbookPart, WorksheetPart worksheetPart) 
            : base(sheet, workbookPart, worksheetPart)
        {
            
        }

        protected override int ColumnStartNumber
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override PlanSheetData GetStructure(string name, IEnumerable<Row> rows, ICollection<int> years, ICollection<int> monthes)
        {
            throw new NotImplementedException();
        }

        protected override ICollection<int> ParseMonthes()
        {
            throw new NotImplementedException();
        }

        protected override ICollection<int> ParseYears()
        {
            throw new NotImplementedException();
        }
    }
}

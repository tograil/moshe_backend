using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using GenericBackend.Excel.Factory;
using GenericBackend.Excel.Sheets;
using GenericBackend.Excel.Structures;
using GenericBackend.Excel.Tests.Utils;
using NUnit.Framework;

namespace GenericBackend.Excel.Tests.Sheet
{
    [TestFixture]
    public class PlanSheetTests
    {
        private PlanSheet _planSheet;

        [SetUp]
        public void PrepareSheetToTest()
        {
            var sheetFactory = new SheetFactory(FilesToTests.PlanActualFileName);

            _planSheet =
                sheetFactory.GetSheet<PlanSheet, PlanSheetData>("Plan", 
                (sheet, workbook, worksheet) => new PlanSheet(sheet, workbook, worksheet));
        }

        [Test]
        public void ClassReturnsCorrectStructure()
        {
            //given

            //when
            var resultStructure = _planSheet.Parse();


            //then
            Assert.That(resultStructure.Years.Any(), Is.True);
        }

    }
}

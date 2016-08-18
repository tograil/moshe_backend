using System.Linq;
using GenericBackend.Excel.Factory;
using GenericBackend.Excel.Sheets;
using GenericBackend.Excel.Tests.Utils;
using NUnit.Framework;

namespace GenericBackend.Excel.Tests.Sheet
{
    [TestFixture]
    public class PlanSheetTests
    {
        private PlanSheet _planSheet;
        private const string Name = "Plan";

        [SetUp]
        public void PrepareSheetToTest()
        {
            var sheetFactory = new SheetFactory(FilesToTests.Actual);

            _planSheet =
                sheetFactory.GetSheet(Name, (sheet, workbook, worksheet) => new PlanSheet(sheet, workbook, worksheet));
        }

        [Test]
        public void ClassReturnsCorrectStructure()
        {
            //given

            //when
            var resultStructure = _planSheet.Parse();


            //then
            Assert.That(resultStructure.Name, Is.EqualTo(Name.ToLowerInvariant()));
            Assert.That(resultStructure.Years.Count(), Is.EqualTo(resultStructure.Monthes.Count()));
            Assert.That(resultStructure.Elements.Any(), Is.True);
            foreach (var sheetItem in resultStructure.Elements)
            {
                foreach (var key in sheetItem.Data.Keys)
                {
                    Assert.That(sheetItem.Data[key].Count, Is.EqualTo(resultStructure.Monthes.Count()));
                }
            }
            
        }

    }
}

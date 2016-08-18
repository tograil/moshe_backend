using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;

namespace GenericBackend.Excel.Tests.Utils
{
    public static class FilesToTests
    {

        public const string PlanActualFileName = @"FilesToParse\PlanActual.xlsx";

        public static string Actual => AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"..\..\..\" + PlanActualFileName;
    }
}

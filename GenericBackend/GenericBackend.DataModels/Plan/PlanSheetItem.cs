using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Core;
using GenericBackend.DataModels.Actual;

namespace GenericBackend.DataModels.Plan
{
    public class PlanSheetItem : MongoEntityBase
    {
        public string Subject { get; set; }
        public long FirstUknknown { get; set; }
        public long SecondUknknown { get; set; }
        public long ThirdUknknown { get; set; }
        public long Nis { get; set; }
        public long CummulativePActualEachMonth { get; set; }
        public long CummulativePlan { get; set; }
        public long Diff { get; set; }
        public List<PlanTimelineData> TimelineData { get; set; }
    }
}

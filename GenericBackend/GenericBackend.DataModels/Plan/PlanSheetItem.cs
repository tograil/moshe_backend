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
        public IEnumerable<PlanTimelineData> TimelineData { get; set; }
    }
}

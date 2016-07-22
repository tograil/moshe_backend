using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Plan
{
    public class PlanTimelineData : MongoEntityBase
    {
        public DateTime DateTime { get; set; }
        public long Plan { get; set; }
        public long AccumulatedPlan { get; set; }
        public string SupervisorComments { get; set; }
    }
}

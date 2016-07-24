using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Plan
{
    public class PlanTimelineData : MongoEntityBase
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Plan { get; set; }
        public string AccumulatedPlan { get; set; }
        public string SupervisorComments { get; set; }
    }
}

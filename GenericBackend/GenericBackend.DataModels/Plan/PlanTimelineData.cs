using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Plan
{
    public class PlanTimelineData : MongoEntityBase
    {
        public DateTime DateTime { get; set; }
        public decimal? Plan { get; set; }
        public decimal? AccumulatedPlan { get; set; }
        public string SupervisorComments { get; set; }
    }
}

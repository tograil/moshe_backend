using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Actual
{
    public class ActualTimelineData : MongoEntityBase
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Actual { get; set; }
        public string UpdateActual { get; set; }
        public string AccumulatedActual { get; set; }
        public string AccumulatedUpdate { get; set; }
        public string SupervisorComments { get; set; }
    }
}

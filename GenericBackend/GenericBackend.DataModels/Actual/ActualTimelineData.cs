using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Actual
{
    public class ActualTimelineData : MongoEntityBase
    {
        public DateTime DateTime { get; set; }
        public long Actual { get; set; }
        public long UpdateActual { get; set; }
        public long AccumulatedActual { get; set; }
        public long AccumulatedUpdate { get; set; }
        public string SupervisorComments { get; set; }
    }
}

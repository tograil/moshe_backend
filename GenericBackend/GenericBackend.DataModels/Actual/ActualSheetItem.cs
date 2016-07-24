using System.Collections.Generic;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Actual
{
    public class ActualSheetItem : MongoEntityBase
    {
        public string Subject { get; set; }
        public IEnumerable<ActualTimelineData> TimelineData { get; set; }
    }
}

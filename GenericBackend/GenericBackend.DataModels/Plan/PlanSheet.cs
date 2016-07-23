using System.Collections.Generic;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Plan
{
    public class PlanSheet : MongoEntityBase
    {
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public List<PlanSheetItem> PlanItems { get; set; }
    }
}

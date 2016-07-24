using System;
using GenericBackend.Core;
using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;

namespace GenericBackend.DataModels.Document
{
    public class DocumentInfo : MongoEntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateOfPost { get; set; }
        public string User { get; set; }
        public PlanSheet Plan { get; set; }
        public ActualSheet Actual { get; set; }

    }
}

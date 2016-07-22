using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;

namespace GenericBackend.Repository.Sheets
{
    public class PlanSheetRepository : MongoRepository<PlanSheet>, IMongoRepository<PlanSheet>
    {
    }
}

using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;
using GenericBackend.DataModels.Total;
using GenericBackend.Repository;
using PlanSheet = GenericBackend.DataModels.Plan.PlanSheet;

namespace GenericBackend.UnitOfWork.GoodNightMedical
{
    public interface IUnitOfWork
    {
        IMongoRepository<DataModels.Actual.ActualSheet> ActualSheets { get; }
        IMongoRepository<PlanSheet> PlanSheets { get; }
        IMongoRepository<TotalSheet> TotalSheets { get; }
    }
}

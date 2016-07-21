using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;
using GenericBackend.DataModels.Total;
using GenericBackend.Repository;

namespace GenericBackend.UnitOfWork.GoodNightMedical
{
    public interface IUnitOfWork
    {
        IMongoRepository<ActualSheet> ActualSheets { get; }
        IMongoRepository<PlanSheet> PlanSheets { get; }
        IMongoRepository<TotalSheet> TotalSheets { get; }
    }
}

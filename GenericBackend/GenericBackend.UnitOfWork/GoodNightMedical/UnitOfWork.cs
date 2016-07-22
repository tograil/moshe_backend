using GenericBackend.DataModels;
using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;
using GenericBackend.DataModels.Total;
using GenericBackend.Repository;
using GenericBackend.Repository.Admin;
using GenericBackend.Repository.Sheets;

namespace GenericBackend.UnitOfWork.GoodNightMedical
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMongoRepository<ActualSheet> _actualSheets;
        private IMongoRepository<PlanSheet> _planSheets;
        private IMongoRepository<TotalSheet> _totalSheets;

        private UserRepository _users; 

        public IMongoRepository<ActualSheet> ActualSheets => _actualSheets ?? (_actualSheets = new ActualSheetRepository());

        public IMongoRepository<PlanSheet> PlanSheets => _planSheets ?? (_planSheets = new PlanSheetRepository());

        public IMongoRepository<TotalSheet> TotalSheets => _totalSheets ?? (_totalSheets = new TotalSheetRepository());

        public UserRepository Users => _users ?? (_users = new UserRepository());
    }
}
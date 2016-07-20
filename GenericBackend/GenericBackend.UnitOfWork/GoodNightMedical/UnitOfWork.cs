using GenericBackend.DataModels.GoodNightMedical;
using GenericBackend.Repository;
using GenericBackend.Repository.GoodNightMedical;

namespace GenericBackend.UnitOfWork.GoodNightMedical
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMongoRepository<Machine> _machines;
        private IMongoRepository<RentOption> _rentOptions;
        private IMongoRepository<MachineOrder> _machineOrders;
        private IMongoRepository<Customer> _customers; 
        private IMongoRepository<FullRentCustomer> _fullRentCustomers; 

        public IMongoRepository<Machine> Machines => _machines ?? (_machines = new MachineRepository());

        public IMongoRepository<RentOption> RentOptions => _rentOptions ?? (_rentOptions = new RentOptionRepository());

        public IMongoRepository<MachineOrder> MachineOrders => _machineOrders ?? (_machineOrders = new MachineOrderRepository());

        public IMongoRepository<Customer> Customers => _customers ?? (_customers = new CustomerRepository());
        public IMongoRepository<FullRentCustomer> FullRentCustomers => _fullRentCustomers ?? (_fullRentCustomers = new FullRentCustomerRepository());
    }
}
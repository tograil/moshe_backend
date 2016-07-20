using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.DataModels.GoodNightMedical;
using GenericBackend.Repository;

namespace GenericBackend.UnitOfWork.GoodNightMedical
{
    public interface IUnitOfWork
    {
        IMongoRepository<Machine> Machines { get; }
        IMongoRepository<RentOption> RentOptions { get; }
        IMongoRepository<MachineOrder> MachineOrders { get; }
        IMongoRepository<Customer> Customers { get; } 
        IMongoRepository<FullRentCustomer> FullRentCustomers { get; } 

    }
}

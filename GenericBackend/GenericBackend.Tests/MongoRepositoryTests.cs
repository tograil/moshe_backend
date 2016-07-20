using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.DataModels.GoodNightMedical;
using GenericBackend.Repository;
using NUnit.Framework;

namespace GenericBackend.Tests
{
    [TestFixture]
    public class MongoRepositoryTests
    {
        [SetUp]
        public void Init()
        {
            
        }
        [Test]
        public void Add_ForMongoDb_ShouldAddEntity_Test()
        {
            var repo = new MongoRepository<Machine>();
            var machineName = "TestMachine";
            
            var machine = new Machine()
            {
                Name = machineName ,
                ImageUrl = "http://www.goodnightmedical.com/images/cpap-machine-photos/Fisher-Paykel-Icon1.jpg",
                PricePerMonth = 100m
            };
            /*var rentOptions = new List<RentOption>();
            rentOptions.Add(new RentOption() { Name = "Rent 1", PricePerMonth = 100m });
            rentOptions.Add(new RentOption() { Name = "Rent 2", PricePerMonth = 200m });
            rentOptions.Add(new RentOption() { Name = "Rent 3", PricePerMonth = 300m });
            machine.RentOptions = rentOptions;*/
            repo.Add(machine);
            
            Assert.That(repo.Any(x => x.Name == machineName));
        }
    }
}




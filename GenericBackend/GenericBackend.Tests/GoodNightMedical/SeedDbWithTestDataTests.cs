using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.DataModels.GoodNightMedical;
using GenericBackend.Repository;
using NUnit.Framework;

namespace GenericBackend.Tests.GoodNightMedical
{
    [TestFixture]
    public class SeedDbWithTestDataTests
    {
        public void RentOptions()
        {
            
        }
        [Test]
        public void Seed()
        {
            var resMedCompany = new Company {Name = "ResMed"};
            var philipsCompany = new Company { Name = "Philips" };
            var fisherCompany = new Company {Name = "Fisher & Paykel"};
            var resMedS10 = new Machine
            {
                ImageUrl = "resmed-s10-airsense-auto-pro.jpg",
                Name = "ResMed S10 AirSense Auto/Pro",
                PricePerMonth = 65,
                Company = resMedCompany
            };

            var resMedS10Ref = new Machine
            {
                ImageUrl = "resmed-s10-airsense-auto-pro.jpg",
                Name = "ResMed S10 AirSense Auto",
                PricePerMonth = 45,
                Refurbished = true,
                Company = resMedCompany
            };
            var dreamStation = new Machine
            {
                Name = "Philips Respironics DreamStation Auto/Pro",
                PricePerMonth = 65,
                ImageUrl = "philips-respironics-dreamstation-auto-pro.jpg",
                Company = philipsCompany
            };

            var remStar = new Machine
            {
                Name = "Philips Respironics RemStar System One Auto",
                PricePerMonth = 45,
                Refurbished = true,
                ImageUrl = "philips-respironics-remstar-system-one-auto.jpg",
                Company = philipsCompany
            };

            var fish = new Machine
            {
                Name = "Fisher & Paykel ICON Premo/Auto",
                PricePerMonth = 49,
                ImageUrl = "fisher-and-paykel-icon-premo-auto.jpg",
                Company = fisherCompany
            };

            var fishRef = new Machine
            {
                Name = "Fisher & Paykel ICON Auto",
                PricePerMonth = 29,
                ImageUrl = "fisher-and-paykel-icon-auto.jpg",
                Refurbished = true,
                Company = fisherCompany

            };

            var resMedS9 = new Machine
            {
                Name = "ResMed S9 Auto/CPAP",
                PricePerMonth = 29,
                ImageUrl = "resmed-s9-auto-cpap.jpg",
                Refurbished = true,
                Company = resMedCompany
            };
            var systemOne = new Machine
            {
                Name = "Philips Respironics System One Auto",
                PricePerMonth = 29,
                Refurbished = true,
                ImageUrl = "philips-respironics-system-one-auto.jpg",
                Company = philipsCompany
            };

            var biPap = new Machine
            {
                Name = "Philips Respironics DreamStation BiPAP",
                PricePerMonth = 75,
                ImageUrl = "philips-respironics-dreamstation-bipap.jpg",
                Company = philipsCompany

            };

            var repo = new MongoRepository<Machine>();
            repo.Add(resMedS10);
            repo.Add(resMedS10Ref);
            repo.Add(dreamStation);
            repo.Add(remStar);
            repo.Add(fish);
            repo.Add(fishRef);
            repo.Add(resMedS9);
            repo.Add(systemOne);
            repo.Add(biPap);












        }
    }
}

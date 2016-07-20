using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Core;


namespace GenericBackend.DataModels.GoodNightMedical
{
    public class Machine : MongoEntityBase, IMongoEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerMonth { get; set; }
        public bool Refurbished { get; set; }
        public Company Company { get; set; }

    }

    //public RentOption RentOption { get; set; }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Core;

namespace GenericBackend.DataModels.GoodNightMedical
{
    public class Customer : MongoEntityBase, IMongoEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool New { get; set; } = true;

    }
}

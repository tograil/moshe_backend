using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.DataModels.GoodNightMedical;

namespace GenericBackend.Repository.GoodNightMedical
{
    public class MachineRepository : MongoRepository<Machine>, IMongoRepository<Machine>
    {
        
    }
}




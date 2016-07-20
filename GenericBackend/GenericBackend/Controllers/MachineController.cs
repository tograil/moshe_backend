using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GenericBackend.DataModels.GoodNightMedical;
using GenericBackend.Providers;
using GenericBackend.Repository;
using GenericBackend.UnitOfWork.GoodNightMedical;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GenericBackend.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/machines")]
    public class MachineController : ApiController
    {
        private readonly IMongoRepository<Machine> _machinesRepository;
        private readonly IMongoRepository<FullRentCustomer> _rentsRepository;

        public MachineController(IUnitOfWork unitOfWork)
        {
            _machinesRepository = unitOfWork.Machines;
            _rentsRepository = unitOfWork.FullRentCustomers;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var machines = await _machinesRepository.Collection.Find(new BsonDocument()).ToListAsync();

            return Ok(machines);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            var machine = _machinesRepository.GetById(id);

            return Ok(machine);
        }

        [HttpGet]
        [Route("rents/seen/{id}")]
        public IHttpActionResult SetSeen(string id)
        {
            var customer = _rentsRepository.GetById(id);

            if (customer == null) return Ok();

            customer.New = false;
            _rentsRepository.Update(customer);

            return Ok();
        }

        [HttpGet]
        [Route("rents")]
        public async Task<IHttpActionResult> GetRentAsks()
        {
            var rents = await _rentsRepository.Collection.Find(new BsonDocument()).ToListAsync();

            return Ok(rents);
        }
    }
}

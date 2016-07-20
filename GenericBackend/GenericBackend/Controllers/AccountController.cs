using System.Threading.Tasks;
using System.Web.Http;
using GenericBackend.Core.Utils;
using GenericBackend.Identity;
using GenericBackend.Identity.Core;
using GenericBackend.Identity.Identity;
using GenericBackend.Models;

namespace GenericBackend.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IApplicationUserManager _userManager;

        public AccountController()
        {
            _userManager = new ApplicationUserManager(new UserStore<IdentityUser>(MongoUtil<IdentityUser>.GetDefaultConnectionString()));
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IHttpActionResult> Registration([FromBody]RegistrationModel model)
        {
            var identityUser = new IdentityUser {UserName = model.Email};

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            return Ok();
        }
    }
}

using System.Collections.Generic;
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

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Get()
        {
            
            return Ok(UserModel.GetUserInfo(User));
        }
        [HttpPost]
        [Route("registration")]
        public async Task<IHttpActionResult> Registration([FromBody]RegistrationModel model)
        {
            var roles = new List<string>();
            //roles.Add("SuperUser");
            var identityUser = new IdentityUser {UserName = model.Email, Roles = roles};

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            return Ok();
        }
    }
}

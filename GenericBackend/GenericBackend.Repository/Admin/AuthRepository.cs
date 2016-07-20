using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.DataModels;

namespace GenericBackend.Repository.Admin
{
    public class AuthRepository : IDisposable
    {
        public Task<IdentityUser> FindUser(string login, string password)
        {
            if(login.Equals("Admin", StringComparison.InvariantCultureIgnoreCase) && password.Equals("KingAdmin"))
                return Task.Run(() => new IdentityUser());

            return Task.Run(() => (IdentityUser)null);
        }

        public void Dispose()
        {
            
        }
    }
}

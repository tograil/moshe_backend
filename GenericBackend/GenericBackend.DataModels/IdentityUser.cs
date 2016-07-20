using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBackend.DataModels
{
    public class IdentityUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}

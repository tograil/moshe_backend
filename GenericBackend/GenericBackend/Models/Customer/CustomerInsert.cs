using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericBackend.Models.Customer
{
    public class CustomerInsert : ModelBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
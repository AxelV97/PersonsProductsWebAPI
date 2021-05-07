using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonsProductsWebAPI.Models
{
    public class PersonComplete
    {
        public Person Person { get; set; }
        public Address Address { get; set; }
        public Product Product { get; set; }
    }
}
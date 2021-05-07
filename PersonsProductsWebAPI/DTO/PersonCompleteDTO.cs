using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonsProductsWebAPI.DTO;

namespace PersonsProductsWebAPI.DTO
{
    public class PersonCompleteDTO
    {
        public PersonDTO Person { get; set; }
        public AddressDTO Address { get; set; }
        public ProductDTO Product { get; set; }
    }
}
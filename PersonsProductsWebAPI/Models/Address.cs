using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonsProductsWebAPI.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        public string AddressLine2 { get; set; }

        [Required]
        [StringLength(255)]
        public string StateOfProvince { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string PostalCode { get; set; }

        public Person Person { get; set; }

        public int PersonId { get; set; }
    }
}
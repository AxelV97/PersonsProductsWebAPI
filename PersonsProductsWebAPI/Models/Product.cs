using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonsProductsWebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Number { get; set; }

        [Required]
        public long NumberInStock { get; set; }

        [Required]
        public decimal StandardCost { get; set; }

        Person Person { get; set; }
        public int PersonId { get; set; }
    }
}
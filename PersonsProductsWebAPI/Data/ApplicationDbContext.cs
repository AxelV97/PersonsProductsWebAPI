using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PersonsProductsWebAPI.Models;

namespace PersonsProductsWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
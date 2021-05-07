using AutoMapper;
using PersonsProductsWebAPI.Data;
using PersonsProductsWebAPI.DTO;
using PersonsProductsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PersonsProductsWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ProductsController()
        {
            this._context = new ApplicationDbContext();
        }

        //GET api/persons
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var products = _context.Products.ToList().Select(Mapper.Map<Product, ProductDTO>);

            return Ok(products);
        }

        //GET api/persons/id
        [HttpGet]
        public IHttpActionResult GetProduct(int Id)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == Id);

            if (productInDb == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Product, ProductDTO>(productInDb));
        }

        //POST api/persons/
        [HttpPost]
        public IHttpActionResult CreateProduct(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productInDb = Mapper.Map<ProductDTO, Product>(productDTO);

            _context.Products.Add(productInDb);
            _context.SaveChanges();

            productDTO.Id = productInDb.Id;

            return Created(new Uri(Request.RequestUri + "/" + productInDb.Id), productDTO);
        }

        //PUT api/persons/
        [HttpPut]
        public IHttpActionResult UpdateProduct(int Id, ProductDTO productDTO)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == Id);

            if (productInDb == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Mapper.Map<ProductDTO, Product>(productDTO, productInDb);
            _context.SaveChanges();

            return Ok("The record was updated successfully!");
        }

        //DELETE api/persons/id
        [HttpDelete]
        public IHttpActionResult DeleteAddress(int Id)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.Id == Id);

            if (productInDb == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productInDb);
            _context.SaveChanges();

            return Ok("The record was deleted successfully!");
        }
    }
}
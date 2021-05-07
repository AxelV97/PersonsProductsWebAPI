using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonsProductsWebAPI.Models;
using PersonsProductsWebAPI.DTO;
using PersonsProductsWebAPI.Data;
using AutoMapper;

namespace PersonsProductsWebAPI.Services
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository()
        {
            this._context = new ApplicationDbContext();
        }


        public ProductDTO GetProduct(int Id)
        {
            var productDTO = _context.Products.SingleOrDefault(p => p.PersonId == Id);

            if (productDTO == null)
            {
                return null;
            }

            return Mapper.Map<Product, ProductDTO>(productDTO);
        }

        public ProductDTO CreateProduct(ProductDTO productDTO)
        {
            var productInDb = Mapper.Map<ProductDTO, Product>(productDTO);

            _context.Products.Add(productInDb);
            _context.SaveChanges();

            productDTO.Id = productInDb.Id;

            return productDTO;
        }

        public ProductDTO UpdateProduct(int Id, ProductDTO productDTO)
        {
            var productInDb = _context.Products.SingleOrDefault(p => p.PersonId == Id);

            if (productInDb == null)
            {
                return null;
            }

            Mapper.Map<ProductDTO, Product>(productDTO, productInDb);
            _context.SaveChanges();

            productDTO.Id = productInDb.Id;

            return productDTO;
        }

        public bool DeleteProduct(int Id)
        {
            var addressInDb = _context.Adresses.SingleOrDefault(p => p.PersonId == Id);

            if (addressInDb == null)
            {
                return false;
            }

            _context.Adresses.Remove(addressInDb);
            _context.SaveChanges();

            return true;
        }
    }
}
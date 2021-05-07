using PersonsProductsWebAPI.Data;
using PersonsProductsWebAPI.Models;
using PersonsProductsWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;

namespace PersonsProductsWebAPI.Controllers
{
    public class AddressesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AddressesController()
        {
            this._context = new ApplicationDbContext();
        }

        //GET api/persons
        [HttpGet]
        public IHttpActionResult GetAddresses()
        {
            var addresses = _context.Adresses.ToList().Select(Mapper.Map<Address, AddressDTO>);

            return Ok(addresses);
        }

        //GET api/persons/id
        [HttpGet]
        public IHttpActionResult GetAddress(int Id)
        {
            var addressInDb = _context.Adresses.SingleOrDefault(p => p.Id == Id);

            if (addressInDb == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Address, AddressDTO>(addressInDb));
        }

        //POST api/persons/
        [HttpPost]
        public IHttpActionResult CreateAddress(AddressDTO addressDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var addressInDb = Mapper.Map<AddressDTO, Address>(addressDTO);

            _context.Adresses.Add(addressInDb);
            _context.SaveChanges();

            addressDTO.Id = addressInDb.Id;

            return Created(new Uri(Request.RequestUri + "/" + addressDTO.Id), addressDTO);
        }

        //PUT api/persons/
        [HttpPut]
        public IHttpActionResult UpdateAddress(int Id, AddressDTO addressDTO)
        {
            var addressInDb = _context.Adresses.SingleOrDefault(p => p.Id == Id);

            if (addressInDb == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Mapper.Map<AddressDTO, Address>(addressDTO, addressInDb);
            _context.SaveChanges();

            return Ok("The record was updated successfully!");
        }

        //DELETE api/persons/id
        [HttpDelete]
        public IHttpActionResult DeleteAddress(int Id)
        {
            var addressInDb = _context.Adresses.SingleOrDefault(p => p.Id == Id);

            if (addressInDb == null)
            {
                return NotFound();
            }

            _context.Adresses.Remove(addressInDb);
            _context.SaveChanges();

            return Ok("The record was deleted successfully!");
        }
    }
}
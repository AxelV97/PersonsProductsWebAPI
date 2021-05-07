using PersonsProductsWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using PersonsProductsWebAPI.Services;
using PersonsProductsWebAPI.DTO;
using PersonsProductsWebAPI.Models;
using AutoMapper;
using System.Data.Entity;

namespace PersonsProductsWebAPI.Controllers
{
    public class PersonsCompleteController : ApiController
    {
        private readonly ApplicationDbContext _context;

        private readonly PersonRepository personRepository;
        private readonly AddressRepository addressRepository;
        private readonly ProductRepository productRepository;
        public PersonsCompleteController()
        {
            personRepository = new PersonRepository();
            addressRepository = new AddressRepository();
            productRepository = new ProductRepository();

            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetPersons()
        {
            var lPersonComplete = (from per in _context.Persons
                                   join add in _context.Adresses on per.Id equals add.PersonId
                                   join prod in _context.Products on per.Id equals prod.PersonId
                                   select new PersonComplete
                                   {
                                       Person = per,
                                       Address = add,
                                       Product = prod
                                   }).ToList()
                                   .Select(Mapper.Map<PersonComplete, PersonCompleteDTO>);

            return Ok(lPersonComplete);
        }

        [HttpGet]
        public IHttpActionResult GetPerson(int Id)
        {
            PersonCompleteDTO personCompleteDTO = new PersonCompleteDTO();
            personCompleteDTO.Person = personRepository.GetPerson(Id);
            personCompleteDTO.Address = addressRepository.GetAddress(Id);
            personCompleteDTO.Product = productRepository.GetProduct(Id);

            return Ok(personCompleteDTO);
        }

        [HttpPost]
        public IHttpActionResult CreatePerson(PersonCompleteDTO personCompleteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var personDTOCreated = personRepository.CreatePerson(personCompleteDTO.Person);

            if (personDTOCreated == null)
            {
                //return NotFound();
                return BadRequest();
            }

            personCompleteDTO.Address.PersonId = personDTOCreated.Id;

            var addressDTOCreated = addressRepository.CreateAddress(personCompleteDTO.Address);

            if (addressDTOCreated == null)
            {
                //return NotFound();
                return BadRequest();
            }

            personCompleteDTO.Product.PersonId = personDTOCreated.Id;

            var productDTOCreated = productRepository.CreateProduct(personCompleteDTO.Product);

            if (productDTOCreated == null)
            {
                return BadRequest();
            }

            return Ok("Record was added successfully!");
        }

        [HttpPut]
        public IHttpActionResult PutPerson(int Id, PersonCompleteDTO personCompleteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var personDTOCreated = personRepository.UpdatePerson(Id, personCompleteDTO.Person);

            if (personDTOCreated == null)
            {
                return NotFound();
            }

            personCompleteDTO.Address.PersonId = personDTOCreated.Id;

            var addressDTOCreated = addressRepository.UpdateAddress(Id, personCompleteDTO.Address);

            if (addressDTOCreated == null)
            {
                return NotFound();
            }

            personCompleteDTO.Product.PersonId = personDTOCreated.Id;

            var productDTOCreated = productRepository.UpdateProduct(Id, personCompleteDTO.Product);

            if (productDTOCreated == null)
            {
                return NotFound();
            }

            return Ok("Record was updated successfully!");
        }

        [HttpDelete]
        public IHttpActionResult DeletePerson(int Id)
        {
            bool success = false;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            success = personRepository.DeletePerson(Id);

            if (!success)
            {
                return NotFound();
            }

            /*The rest of the entities are eliminated automatically when deleting the primary record*/

            ////personCompleteDTO.Address.PersonId = personDTOCreated.Id;

            //success = addressRepository.DeleteAddress(Id);

            //if (!success)
            //{
            //    //return NotFound();
            //    return BadRequest();
            //}

            ////personCompleteDTO.Product.PersonId = personDTOCreated.Id;

            //success = productRepository.DeleteProduct(Id);

            //if (!success)
            //{
            //    //return NotFound();
            //    return BadRequest();
            //}

            return Ok("Record was deleted successfully!");
        }
    }
}
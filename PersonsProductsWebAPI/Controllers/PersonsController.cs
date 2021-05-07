using PersonsProductsWebAPI.Data;
using PersonsProductsWebAPI.Models;
using PersonsProductsWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;

namespace PersonsProductsWebAPI.Controllers
{
    public class PersonsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public PersonsController()
        {
            this._context = new ApplicationDbContext();
        }

        //GET api/persons
        [HttpGet]
        public IHttpActionResult GetPersons()
        {
            var persons = _context.Persons.ToList().Select(Mapper.Map<Person, PersonDTO>);

            return Ok(persons);
        }

        //GET api/persons/id

        public IHttpActionResult GetPerson(int Id)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Id == Id);

            if (personInDb == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Person, PersonDTO>(personInDb));
        }

        //POST api/persons/
        [HttpPost]
        public IHttpActionResult CreatePerson(PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var personInDb = Mapper.Map<PersonDTO, Person>(personDTO);

            _context.Persons.Add(personInDb);
            _context.SaveChanges();

            personDTO.Id = personInDb.Id;

            return Created(new Uri(Request.RequestUri + "/" + personInDb.Id), personDTO);
        }

        //PUT api/persons/
        [HttpPut]
        public IHttpActionResult UpdatePerson(int Id, PersonDTO personDTO)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Id == Id);

            if (personInDb == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Mapper.Map<PersonDTO, Person>(personDTO, personInDb);
            _context.SaveChanges();

            return Ok("The record was updated successfully!");
        }

        //DELETE api/persons/id
        [HttpDelete]
        public IHttpActionResult DeletePerson(int Id)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Id == Id);

            if (personInDb == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(personInDb);
            _context.SaveChanges();

            return Ok("The record was deleted successfully!");
        }
    }
}
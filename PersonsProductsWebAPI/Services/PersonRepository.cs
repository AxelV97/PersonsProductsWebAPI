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
    public class PersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository()
        {
            this._context = new ApplicationDbContext();
        }

        public PersonDTO GetPerson(int Id)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Id == Id);

            if (personInDb == null)
            {
                return null;
            }

            return Mapper.Map<Person, PersonDTO>(personInDb);
        }

        public PersonDTO CreatePerson(PersonDTO personDTO)
        {
            var personInDb = Mapper.Map<PersonDTO, Person>(personDTO);

            _context.Persons.Add(personInDb);
            _context.SaveChanges();

            personDTO.Id = personInDb.Id;

            return personDTO;
        }

        public PersonDTO UpdatePerson(int Id, PersonDTO personDTO)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Id == Id);

            if (personInDb == null)
            {
                return null;
            }

            Mapper.Map<PersonDTO, Person>(personDTO, personInDb);
            _context.SaveChanges();

            personDTO.Id = personInDb.Id;

            return personDTO;
        }

        public bool DeletePerson(int Id)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Id == Id);

            if (personInDb == null)
            {
                return false;
            }

            _context.Persons.Remove(personInDb);
            _context.SaveChanges();

            return true;
        }

    }
}
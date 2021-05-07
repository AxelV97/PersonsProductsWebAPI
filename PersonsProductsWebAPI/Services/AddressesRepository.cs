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
    public class AddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository()
        {
            this._context = new ApplicationDbContext();
        }

        public AddressDTO GetAddress(int Id)
        {
            var addressInDb = _context.Adresses.SingleOrDefault(p => p.PersonId == Id);

            if (addressInDb == null)
            {
                return null;
            }

            return Mapper.Map<Address, AddressDTO>(addressInDb);
        }

        public AddressDTO CreateAddress(AddressDTO addressDTO)
        {
            var addressInDb = Mapper.Map<AddressDTO, Address>(addressDTO);

            _context.Adresses.Add(addressInDb);
            _context.SaveChanges();

            addressDTO.Id = addressInDb.Id;

            return addressDTO;
        }

        public AddressDTO UpdateAddress(int Id, AddressDTO addressDTO)
        {
            var addressInDb = _context.Adresses.SingleOrDefault(p => p.PersonId == Id);

            if (addressInDb == null)
            {
                return null;
            }

            Mapper.Map<AddressDTO, Address>(addressDTO, addressInDb);
            _context.SaveChanges();

            addressDTO.Id = addressInDb.Id;

            return addressDTO;
        }

        public bool DeleteAddress(int Id)
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
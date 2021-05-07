using PersonsProductsWebAPI.DTO;
using PersonsProductsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace PersonsProductsWebAPI.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /* Domain to DTO */
            Mapper.CreateMap<Person, PersonDTO>();

            Mapper.CreateMap<Address, AddressDTO>();

            Mapper.CreateMap<Product, ProductDTO>();

            Mapper.CreateMap<PersonComplete, PersonCompleteDTO>();

            /*DTO to Domain*/
            Mapper.CreateMap<PersonDTO, Person>()
                .ForMember(p => p.Id, opt => opt.Ignore());

            Mapper.CreateMap<AddressDTO, Address>()
                .ForMember(a => a.Id, opt => opt.Ignore());

            Mapper.CreateMap<ProductDTO, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}
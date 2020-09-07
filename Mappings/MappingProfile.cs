using AutoMapper;
using System;
using CustomerManagement.Entity;
using CustomerManagement.Models;

namespace CustomerManagement.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerEntity>().ReverseMap();
            CreateMap<Address, AddressEntity>().ReverseMap();
        }
    }
}

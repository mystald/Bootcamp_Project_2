using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AdminService.Dtos;

namespace AdminService.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, DtoCustomerGet>()
                .ForMember(dest => dest.name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<DtoCustomerGet, CustomerDto>();
        }


    }
}
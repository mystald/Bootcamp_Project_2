using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerService.Dtos;
using CustomerService.Models;

namespace CustomerService.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetCustomerDto>()
                .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<GetCustomerForCreateDto, Customer>();

            CreateMap<DtoOrderOutput, DtoOrderForKafka>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.StartingPoint, opt => opt.MapFrom(src => src.startDest))
                .ForMember(dest => dest.Destination, opt => opt.MapFrom(src => src.endDest));
        }
    }
}
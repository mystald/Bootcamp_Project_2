using System;
using AutoMapper;
using DriverService.Dtos;
using DriverService.Models;

namespace DriverService.Profiles
{
    public class DriversProfile : Profile
    {
        public DriversProfile()
        {
         CreateMap<Driver, DriverDto>()
         .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
         CreateMap<UpdateForPositionDto, Driver>();
         CreateMap<Driver, GetDriverBalanceDto>()
         .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
         CreateMap<Driver, GetDriverProfileDto>()
         .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => DateTime.Today.Year - src.BirthDate.Year));  
        CreateMap<AcceptDriverDto, Driver>(); 
        }
    }
}
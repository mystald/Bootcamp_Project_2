using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Dto;
using AutoMapper;

namespace AuthService.Profiles
{
    public class ProfileUser : Profile
    {
        public ProfileUser()
        {
            CreateMap<DtoUserRegisterDriver, DtoUserRegister>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => role.driver));

            CreateMap<DtoUserRegisterCustomer, DtoUserRegister>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => role.customer));
        }
    }
}
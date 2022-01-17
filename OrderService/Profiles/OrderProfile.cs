using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetTopologySuite.Geometries;
using OrderService.Dto;
using OrderService.Models;

namespace OrderService.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<DtoOrderInsert, Order>()
                .ForMember(dst => dst.startDest,
                opt => opt.MapFrom(src => new Point(src.startDest.X, src.startDest.Y) { SRID = 4326 }))
                .ForMember(dst => dst.endDest,
                opt => opt.MapFrom(src => new Point(src.endDest.X, src.endDest.Y) { SRID = 4326 }));
        }
    }
}
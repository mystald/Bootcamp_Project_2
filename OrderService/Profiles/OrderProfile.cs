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

            CreateMap<Order, DtoOrderReturn>()
                .ForMember(dst => dst.startDest,
                opt => opt.MapFrom(src => new location { X = src.startDest.X, Y = src.startDest.Y }))
                .ForMember(dst => dst.endDest,
                opt => opt.MapFrom(src => new location { X = src.endDest.X, Y = src.endDest.Y }))
                .ForMember(dst => dst.Status,
                opt => opt.MapFrom(src => (status)src.Status));
        }
    }
}
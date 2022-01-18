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
                .ForMember(dst => dst.StartDest,
                opt => opt.MapFrom(src => new Point(src.startDest.Lat, src.startDest.Long) { SRID = 4326 }))
                .ForMember(dst => dst.EndDest,
                opt => opt.MapFrom(src => new Point(src.endDest.Lat, src.endDest.Long) { SRID = 4326 }));

            CreateMap<Order, DtoOrderReturn>()
                .ForMember(dst => dst.StartDest,
                opt => opt.MapFrom(src => new location { Lat = src.StartDest.X, Long = src.StartDest.Y }))
                .ForMember(dst => dst.EndDest,
                opt => opt.MapFrom(src => new location { Lat = src.EndDest.X, Long = src.EndDest.Y }))
                .ForMember(dst => dst.Status,
                opt => opt.MapFrom(src => (status)src.Status));

            CreateMap<DtoOrderCheckFeeInsert, Order>()
                .ForMember(dst => dst.StartDest,
                opt => opt.MapFrom(src => new Point(src.StartDest.Lat, src.StartDest.Long) { SRID = 4326 }))
                .ForMember(dst => dst.EndDest,
                opt => opt.MapFrom(src => new Point(src.EndDest.Lat, src.EndDest.Long) { SRID = 4326 }));
        }
    }
}
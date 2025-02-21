using AutoMapper;
using Catalog.Application.Dtos;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mappers
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationCreateDto, Location>();
            CreateMap<Location, LocationDto>();
        }
    }
}

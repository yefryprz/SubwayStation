using AutoMapper;
using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.DTOs.Seed;
using SubwayStation.Domain.Entities;
using SubwayStation.Domain.ViewModels;

namespace SubwayStation.Infrastructure.MapperProfiles
{
    public class SubwayProfile : Profile
    {
        public SubwayProfile()
        {
            CreateMap<Subways, SubwayDTO>().ReverseMap();
            CreateMap<Subways, GeometricDTO>();
            CreateMap<SubwaysSeedDTO, Subways>()
                //Condition to prevent error when station dosen't have name
                .ForMember(dest => dest.Name, opt => opt.MapFrom(mapProp => mapProp.Name ?? ""));


            CreateMap<Geometric, GeometricDTO>().ReverseMap();
            CreateMap<DistancesViewModel, GeometricDTO>();
            CreateMap<GeometricSeedDTO, Geometric>()
                //Separate coord on two column
                .ForMember(dest => dest.Latitude, src => src.MapFrom(o => o.Coordinates.First().ToString()))
                .ForMember(dest => dest.Longitude, src => src.MapFrom(o => o.Coordinates.Last().ToString()));
        }
    }
}

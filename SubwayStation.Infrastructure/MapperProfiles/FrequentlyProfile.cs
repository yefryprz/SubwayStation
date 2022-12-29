using AutoMapper;
using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.Entities;

namespace SubwayStation.Infrastructure.MapperProfiles
{
    public class FrequentlyProfile : Profile
    {
        public FrequentlyProfile()
        {
            CreateMap<Frequently, FrequentlyDTO>().ReverseMap();
        }
    }
}

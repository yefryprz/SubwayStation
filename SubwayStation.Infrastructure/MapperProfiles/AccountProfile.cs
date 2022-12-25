using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SubwayStation.Domain.ViewModels;

namespace SubwayStation.Infrastructure.MapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<IdentityUser, SignUpViewModel>().ReverseMap();
        }
    }
}

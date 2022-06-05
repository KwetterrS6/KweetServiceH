using AutoMapper;
using KweetService.Dtos;
using KweetService.Models;


namespace KweetService.Profiles
{
    public class KweetsProfile : Profile
    {
        public KweetsProfile()
        {
            // Source -> Target
            CreateMap<User, UserReadDto>();
            CreateMap<KweetCreateDto, Kweet>();
            CreateMap<Kweet, KweetReadDto>();
            CreateMap<UserCreatedDto, User>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
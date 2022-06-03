using AutoMapper;
using KweetService.Dtos;
using KweetService.Models;
using UserService;

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
            CreateMap<GrpcUserModel, User>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Kweets, opt => opt.Ignore());
        }
    }
}
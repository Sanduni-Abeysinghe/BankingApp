using AutoMapper;
using BankingSystemAPI.Models;
using BankingSystemAPI.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map from UserDto to User
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        // Map from User to UserDto
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
    }
}

using AutoMapper;
using VowelBonus.Domain.Common;
using VowelBonus.Domain.Entities;

namespace VowelBonus.Application.v1.Users.Profiles;

public class UserProfile : Profile  
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserSaveDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}


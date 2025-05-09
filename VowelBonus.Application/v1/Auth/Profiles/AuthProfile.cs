namespace VowelBonus.Application.v1.Auth.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<VowelBonusScoreHistory, VowelBonusScoreHistoryDto>().ReverseMap();
    }
}

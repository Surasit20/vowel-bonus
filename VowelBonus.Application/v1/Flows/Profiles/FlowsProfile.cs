namespace VowelBonus.Application.v1.VowelBonusScoreHistories.Profiles;

public class FlowsProfile : Profile
{
    public FlowsProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<VowelBonusScoreHistory, VowelBonusScoreHistoryDto>().ReverseMap();
    }
}

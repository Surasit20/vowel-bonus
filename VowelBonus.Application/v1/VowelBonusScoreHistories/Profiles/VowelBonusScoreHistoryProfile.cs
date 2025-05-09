namespace VowelBonus.Application.v1.VowelBonusScoreHistories.Profiles;

public class VowelBonusScoreHistoryProfile : Profile
{
    public VowelBonusScoreHistoryProfile()
    {
        CreateMap<VowelBonusScoreHistory, VowelBonusScoreHistorySaveDto>().ReverseMap();
        CreateMap<VowelBonusScoreHistory, VowelBonusScoreHistoryDto>().ReverseMap();
    }
}

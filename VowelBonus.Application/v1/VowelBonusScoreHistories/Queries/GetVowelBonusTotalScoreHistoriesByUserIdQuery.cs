using VowelBonus.Domain.Interfaces;

namespace VowelBonus.Application.v1.Users;

public record GetVowelBonusTotalScoreHistoriesByUserIdQuery(int UserId) : IRequest<Response<int>>
{

}

public class GetVowelBonusTotalScoreHistoriesByUserIdQueryHandler : IRequestHandler<GetVowelBonusTotalScoreHistoriesByUserIdQuery, Response<int>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly IMapper _mapper;

    public GetVowelBonusTotalScoreHistoriesByUserIdQueryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(GetVowelBonusTotalScoreHistoriesByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<int>();
            var totalScore = await _vowelBonusScoreHistoryRepository.GetTotalPointByUserIdAsync(request.UserId);
            return res.Success(totalScore, BaseConst.GET_DATA_SUCCESS);
        
        }
        catch (Exception ex)
        {
            return new Response<int>().Failure(ex.Message);
        }

    }
}

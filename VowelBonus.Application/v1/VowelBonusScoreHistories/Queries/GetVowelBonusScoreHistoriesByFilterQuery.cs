using VowelBonus.Domain.Interfaces;

namespace VowelBonus.Application.v1.Users;

public record GetVowelBonusScoreHistoriesByFilterQuery(VowelBonusScoreHistoryFilterDto args) : IRequest<Response<IEnumerable<VowelBonusScoreHistoryDto>>>
{
}

public class GetVowelBonusScoreHistoriesByFilterQueryHandler : IRequestHandler<GetVowelBonusScoreHistoriesByFilterQuery, Response<IEnumerable<VowelBonusScoreHistoryDto>>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly IMapper _mapper;

    public GetVowelBonusScoreHistoriesByFilterQueryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<VowelBonusScoreHistoryDto>>> Handle(GetVowelBonusScoreHistoriesByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<IEnumerable<VowelBonusScoreHistoryDto>>();
            var vowelBonusScoreHistory = await _vowelBonusScoreHistoryRepository.GetByFilterAsync(request.args);
            var totalRecord = await _vowelBonusScoreHistoryRepository.GetCountByFilterAsync(request.args);
            
            var vowelBonusScoreHistoryDto = _mapper.Map<IEnumerable<VowelBonusScoreHistory>, IEnumerable<VowelBonusScoreHistoryDto>>(vowelBonusScoreHistory);
            return res.Success(vowelBonusScoreHistoryDto, BaseConst.GET_DATA_SUCCESS, totalRecord);
        
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<VowelBonusScoreHistoryDto>>().Failure(ex.Message);
        }

    }
}

using VowelBonus.Application.Common;

namespace VowelBonus.Application.v1.VowelBonusScoreHistories;

public record CreateVowelBonusScoreHistoryCommand(VowelBonusScoreHistorySaveDto args) : IRequest<Response<VowelBonusScoreHistoryDto>>
{
    public VowelBonusScoreHistorySaveDto Args = args;
}

public class CreateVowelBonusScoreHistoryHandler : IRequestHandler<CreateVowelBonusScoreHistoryCommand, Response<VowelBonusScoreHistoryDto>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly IMapper _mapper;
    private readonly ICalculatePointService _calculatePoint;
    public CreateVowelBonusScoreHistoryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper, ICalculatePointService calculatePoint)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
        _calculatePoint = calculatePoint;
    }

    public async Task<Response<VowelBonusScoreHistoryDto>> Handle(CreateVowelBonusScoreHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<VowelBonusScoreHistoryDto>();
            var args = request.Args;
            var vowelBonusScoreHistory = _mapper.Map<VowelBonusScoreHistorySaveDto,VowelBonusScoreHistory>(args);
            vowelBonusScoreHistory.Point = _calculatePoint.CalculateVowelPoint(vowelBonusScoreHistory.Word);

            await _vowelBonusScoreHistoryRepository.AddAsync(vowelBonusScoreHistory);

            var vowelBonusScoreHistoryResponseDto = _mapper.Map<VowelBonusScoreHistory, VowelBonusScoreHistoryDto>(vowelBonusScoreHistory);
            return res.Success(vowelBonusScoreHistoryResponseDto, BaseConst.SAVE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<VowelBonusScoreHistoryDto>().Failure(ex.Message);
        }
    }

}

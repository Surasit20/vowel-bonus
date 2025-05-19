using VowelBonus.Application.Common;

namespace VowelBonus.Application.v1.VowelBonusScoreHistories;

public record CreateVowelBonusScoreHistoryCommand(VowelBonusScoreHistorySaveDto args) : IRequest<Response<VowelBonusScoreHistoryResponseDto>>
{
    public VowelBonusScoreHistorySaveDto Args = args;
}

public class CreateVowelBonusScoreHistoryHandler : IRequestHandler<CreateVowelBonusScoreHistoryCommand, Response<VowelBonusScoreHistoryResponseDto>>
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

    public async Task<Response<VowelBonusScoreHistoryResponseDto>> Handle(CreateVowelBonusScoreHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<VowelBonusScoreHistoryResponseDto>();
            var args = request.Args;

            var vowelBonusScoreHistory = _mapper.Map<VowelBonusScoreHistorySaveDto,VowelBonusScoreHistory>(args);
            vowelBonusScoreHistory.Point = _calculatePoint.CalculateVowelPoint(vowelBonusScoreHistory.Word);

            await _vowelBonusScoreHistoryRepository.AddAsync(vowelBonusScoreHistory);
            var totalPoint = await _vowelBonusScoreHistoryRepository.GetTotalPointByUserIdAsync(args.UserId);
            var vowelBonusScoreHistories = await _vowelBonusScoreHistoryRepository.GetByTaskAsync(args.UserId, BaseConst.DEFAULT_TASK);

            var vowelBonusScoreHistoryDto = _mapper.Map<IEnumerable<VowelBonusScoreHistory>, IEnumerable<VowelBonusScoreHistoryDto>>(vowelBonusScoreHistories);

            var vowelBonusScoreHistoryResponseDto = new VowelBonusScoreHistoryResponseDto();
            vowelBonusScoreHistoryResponseDto.TotalPoint = totalPoint;
            vowelBonusScoreHistoryResponseDto.VowelBonusScoreHistories = vowelBonusScoreHistoryDto;

            return res.Success(vowelBonusScoreHistoryResponseDto, BaseConst.SAVE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<VowelBonusScoreHistoryResponseDto>().Failure(ex.Message);
        }
    }

}

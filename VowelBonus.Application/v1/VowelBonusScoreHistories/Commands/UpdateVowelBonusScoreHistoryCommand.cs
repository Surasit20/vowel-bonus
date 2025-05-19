using VowelBonus.Application.Common;

namespace VowelBonus.Application.v1.VowelBonusScoreHistories;

public record UpdateVowelBonusScoreHistoryCommand(VowelBonusScoreHistoryUpdateDto Args) : IRequest<Response<VowelBonusScoreHistoryDto>>
{
    public VowelBonusScoreHistoryUpdateDto Args = Args;
}

public class UpdateVowelBonusScoreHistoryHandler : IRequestHandler<UpdateVowelBonusScoreHistoryCommand, Response<VowelBonusScoreHistoryDto>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly ICalculatePointService _calculatePoint;
    private readonly IMapper _mapper;
    public UpdateVowelBonusScoreHistoryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper, ICalculatePointService calculatePoint)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
        _calculatePoint = calculatePoint;
    }

    public async Task<Response<VowelBonusScoreHistoryDto>> Handle(UpdateVowelBonusScoreHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<VowelBonusScoreHistoryDto>();
            var args = request.Args;

            var vowelBonusScoreHistory = _mapper.Map<VowelBonusScoreHistoryUpdateDto,VowelBonusScoreHistory>(args);
            vowelBonusScoreHistory.Point = _calculatePoint.CalculateVowelPoint(vowelBonusScoreHistory.Word);
            await _vowelBonusScoreHistoryRepository.UpdateAsync(vowelBonusScoreHistory);

            var vowelBonusScoreHistoryDto = _mapper.Map<VowelBonusScoreHistory, VowelBonusScoreHistoryDto>(vowelBonusScoreHistory);
            return res.Success(vowelBonusScoreHistoryDto, BaseConst.SAVE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<VowelBonusScoreHistoryDto>().Failure(ex.Message);
        }
    }
}

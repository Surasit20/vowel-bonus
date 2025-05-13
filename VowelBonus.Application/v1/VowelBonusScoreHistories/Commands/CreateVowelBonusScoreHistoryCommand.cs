namespace VowelBonus.Application.v1.VowelBonusScoreHistories;

public record CreateVowelBonusScoreHistoryCommand(VowelBonusScoreHistorySaveDto args) : IRequest<Response<VowelBonusScoreHistoryResponseDto>>
{
    public VowelBonusScoreHistorySaveDto Args = args;
}

public class CreateVowelBonusScoreHistoryHandler : IRequestHandler<CreateVowelBonusScoreHistoryCommand, Response<VowelBonusScoreHistoryResponseDto>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly IMapper _mapper;
    public CreateVowelBonusScoreHistoryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Response<VowelBonusScoreHistoryResponseDto>> Handle(CreateVowelBonusScoreHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<VowelBonusScoreHistoryResponseDto>();
            var args = request.Args;

            var vowelBonusScoreHistory = _mapper.Map<VowelBonusScoreHistorySaveDto,VowelBonusScoreHistory>(args);
            vowelBonusScoreHistory.Point = CalculateVowelPoint(vowelBonusScoreHistory.Word);

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

    public int CalculateVowelPoint(string input)
    {
        int point = 0;

        input = input.ToLower();
        var len = input.Length - 1;
        for (int i = 0; i <= len; i++)
        {
            if (char.IsLetter(input[i]))
            {
                if (i == 0 && BaseConst.VOWEL_SCORES.ContainsKey(input[i]) && BaseConst.VOWEL_SCORES.ContainsKey(input[i+1]))
                {
                    point += BaseConst.VOWEL_SCORES[input[i]] * 2;
                }
                else if (i == len && BaseConst.VOWEL_SCORES.ContainsKey(input[i]) && BaseConst.VOWEL_SCORES.ContainsKey(input[i - 1]))
                {
                    point += BaseConst.VOWEL_SCORES[input[i]] * 2;
                }
                else if (BaseConst.VOWEL_SCORES.TryGetValue(input[i], out int value1) && 
                                                            i != 0 &&
                                                            i != len &&
                                                            (BaseConst.VOWEL_SCORES.ContainsKey(input[i-1]) || 
                                                            BaseConst.VOWEL_SCORES.ContainsKey(input[i + 1])))
                {
                    point += value1 * 2;
                }
                else if (BaseConst.VOWEL_SCORES.TryGetValue(input[i], out int value2))
                {
                    point += value2;
                }
                else
                {
                    point += 1;
                }
            }
        }

        return point;
    }
}

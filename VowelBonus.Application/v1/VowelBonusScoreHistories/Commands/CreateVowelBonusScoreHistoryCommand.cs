namespace VowelBonus.Application.v1.VowelBonusScoreHistories;

public record CreateVowelBonusScoreHistoryCommand(VowelBonusScoreHistorySaveDto args) : IRequest<Response<VowelBonusScoreHistoryDto>>
{
    public VowelBonusScoreHistorySaveDto Args = args;
}

public class CreateVowelBonusScoreHistoryHandler : IRequestHandler<CreateVowelBonusScoreHistoryCommand, Response<VowelBonusScoreHistoryDto>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly IMapper _mapper;
    public CreateVowelBonusScoreHistoryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Response<VowelBonusScoreHistoryDto>> Handle(CreateVowelBonusScoreHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<VowelBonusScoreHistoryDto>();
            var args = request.Args;

            var vowelBonusScoreHistory = _mapper.Map<VowelBonusScoreHistorySaveDto,VowelBonusScoreHistory>(args);
            vowelBonusScoreHistory.Point = CalculateVowelPoint(vowelBonusScoreHistory.Word);
            await _vowelBonusScoreHistoryRepository.AddAsync(vowelBonusScoreHistory);

            var vowelBonusScoreHistoryDto = _mapper.Map<VowelBonusScoreHistory,VowelBonusScoreHistoryDto>(vowelBonusScoreHistory);
            return res.Success(vowelBonusScoreHistoryDto, BaseConst.SAVE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<VowelBonusScoreHistoryDto>().Failure(ex.Message);
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
                else if (BaseConst.VOWEL_SCORES.TryGetValue(input[i], out int value) && (BaseConst.VOWEL_SCORES.ContainsKey(input[i-1]) || BaseConst.VOWEL_SCORES.ContainsKey(input[i + 1])))
                {
                    point += value * 2;
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

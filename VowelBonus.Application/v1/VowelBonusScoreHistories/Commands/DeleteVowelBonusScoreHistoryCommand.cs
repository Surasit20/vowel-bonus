using VowelBonus.Application.Common;

namespace VowelBonus.Application.v1.VowelBonusScoreHistories;

public record DeleteVowelBonusScoreHistoryCommand(int Id) : IRequest<Response<bool>>
{
    public int Id = Id;
}

public class DeleteVowelBonusScoreHistoryHandler : IRequestHandler<DeleteVowelBonusScoreHistoryCommand, Response<bool>>
{
    private readonly IVowelBonusScoreHistoryRepository _vowelBonusScoreHistoryRepository;
    private readonly IMapper _mapper;
    public DeleteVowelBonusScoreHistoryHandler(IVowelBonusScoreHistoryRepository vowelBonusScoreHistoryRepository, IMapper mapper)
    {
        _vowelBonusScoreHistoryRepository = vowelBonusScoreHistoryRepository;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(DeleteVowelBonusScoreHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<bool>();
            await _vowelBonusScoreHistoryRepository.DeleteAsync(request.Id);
            return res.Success(true, BaseConst.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<bool>().Failure(ex.Message);
        }
    }
}

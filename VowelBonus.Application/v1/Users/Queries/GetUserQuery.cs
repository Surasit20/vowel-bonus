using VowelBonus.Domain.Entities;

namespace VowelBonus.Application.v1.Users;

public record GetUserQuery : IRequest<Response<IEnumerable<UserDto>>>;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Response<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUserRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<UserDto>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<IEnumerable<UserDto>>();
            var users = await _context.GetAllAsync();
            var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);

            return res.Success(usersDto, BaseConst.GET_DATA_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<UserDto>>().Failure(ex.Message);
        }

    }
}

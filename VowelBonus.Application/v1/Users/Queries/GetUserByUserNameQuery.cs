namespace VowelBonus.Application.v1.Users;

public record GetUserByUserNameQuery(string UserName) : IRequest<Response<UserDto>>
{
}

public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, Response<UserDto>>
{
    private readonly IUserRepository _context;
    private readonly IMapper _mapper;

    public GetUserByUserNameQueryHandler(IUserRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<UserDto>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<UserDto>();
            var userName = request.UserName;
            var user = await _context.GetByUserNameAsync(userName);
            if(user != null)
            {
                var userDto = _mapper.Map<User, UserDto>(user);
                return res.Success(userDto, BaseConst.GET_DATA_SUCCESS);
            }
            else
            {
                return res.Failure(BaseConst.NOT_FOUND);
            }
        }
        catch (Exception ex)
        {
            return new Response<UserDto>().Failure(ex.Message);
        }

    }
}

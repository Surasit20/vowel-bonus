namespace VowelBonus.Application.v1.Users;

public record GetUsersByIdQuery(int UserId) : IRequest<Response<UserDto>>
{
}

public class GetUsersByIdQueryHandler : IRequestHandler<GetUsersByIdQuery, Response<UserDto>>
{
    private readonly IUserRepository _context;
    private readonly IMapper _mapper;

    public GetUsersByIdQueryHandler(IUserRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<UserDto>> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<UserDto>();
            int userId = request.UserId;
            var user = await _context.GetByIdAsync(userId);
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

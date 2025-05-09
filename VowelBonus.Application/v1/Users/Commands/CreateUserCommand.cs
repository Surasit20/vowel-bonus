namespace VowelBonus.Application.v1.Users;

public record CreateUserCommand(UserSaveDto Args) : IRequest<Response<UserDto>>
{
    public UserSaveDto Args = Args;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
{
    private readonly IUserRepository _context;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(IUserRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<UserDto>();
            var args = request.Args;
            var user  = _mapper.Map<UserSaveDto, User>(args);
            await _context.AddAsync(user);

            var userDto = _mapper.Map<User, UserDto>(user);

            return res.Success(userDto, BaseConst.SAVE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<UserDto>().Failure(ex.Message);
        }
    }
}

namespace VowelBonus.Application.v1.Users;

public record UpdateUserCommand(UserUpdateDto Args) : IRequest<Response<UserDto>>
{
    public UserUpdateDto Args = Args;
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<UserDto>>
{
    private readonly IUserRepository _context;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<UserDto>();
            var args = request.Args;
            var user = _mapper.Map<UserUpdateDto, User>(args);
            var userUpdate = await _context.UpdateAsync(user);
            var userDto = _mapper.Map<User, UserDto>(userUpdate);

            return res.Success(userDto, BaseConst.UPDATE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<UserDto>().Failure(ex.Message);
        }
    }
}

using VowelBonus.Application.v1.Users;

namespace VowelBonus.Application.Auth;

public record LoginCommand(LoginDto Args) : IRequest<Response<UserDto>>
{
    public LoginDto Args = Args;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, IMediator mediator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Response<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<UserDto>();
            var args = request.Args;

            var isExistsUserName = await _userRepository.ExistsAsync(args.UserName);
            if (isExistsUserName == true)
            {
                var user = await _userRepository.GetByUserNameAsync(args.UserName);
                if (user != null) 
                {
                    var userDto = _mapper.Map<User, UserDto>(user);
                    return res.Success(userDto, BaseConst.SAVE_SUCCESS);
                }
                else
                {
                    return res.Failure(BaseConst.NOT_FOUND);
                }
            }
            else 
            {
                var newUser = await _mediator.Send(new CreateUserCommand(new UserSaveDto() { UserName = args.UserName}));

                if (newUser != null && newUser.Succeeded == true && newUser.Result != null) 
                { 
                    return res.Success(newUser.Result, BaseConst.SAVE_SUCCESS);
                }
                else
                {
                    return res.Failure(BaseConst.ERROR_SUCCESS);
                }
            }
        }
        catch (Exception ex)
        {
            return new Response<UserDto>().Failure(ex.Message);
        }
    }
}

namespace VowelBonus.Application.v1.Users;

public record DeleteUserCommand(int Id) : IRequest<Response<bool>>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<bool>>
{
    private readonly IUserRepository _context;

    public DeleteUserCommandHandler(IUserRepository context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<bool>();
            var user = await _context.GetByIdAsync(request.Id);

            await _context.DeleteAsync(request.Id);
            return res.Success(true, BaseConst.DELETE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<bool>().Failure(ex.Message);
        }
    }
}

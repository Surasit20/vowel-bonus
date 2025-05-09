using VowelBonus.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Controllers.v1;
using System.Reflection;
using VowelBonus.Application.v1.Users;

namespace VowelBonus.API.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        public UsersController(ISender sender) : base(sender)
        {
        }
        [HttpGet("GetUsers", Name = "GetUsers")]
        public async Task<Response<IEnumerable<UserDto>>> GetUsers()
        {
            return await _sender.Send(new GetUserQuery());
        }

        [HttpGet("GetUser/{id}", Name = "GetUserById")]
        public async Task<Response<UserDto>> GetUserById(int id)
        {
            return await _sender.Send(new GetUsersByIdQuery(id));
        }

        [HttpGet("GetUserByUserName/{userName}", Name = "GetUserByUserName")]
        public async Task<Response<UserDto>> GetUserByUserName(string userName)
        {
            return await _sender.Send(new GetUserByUserNameQuery(userName));
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        public async Task<Response<UserDto>> CreateUser([FromBody] UserSaveDto args)
        {
            return await _sender.Send(new CreateUserCommand(args));
        }

        [HttpPatch("UpdateUser", Name = "UpdateUser")]
        public async Task<Response<UserDto>> UpdateUsers(UserUpdateDto args)
        {
           return await _sender.Send(new UpdateUserCommand(args));
        }

        [HttpDelete("DeleteUser/{id}", Name = "DeleteUser")]
        public async Task<Response<bool>> DeleteUser(int Id)
        {
            return await _sender.Send(new DeleteUserCommand(Id));
        }
    }
}   

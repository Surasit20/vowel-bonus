using VowelBonus.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Controllers.v1;
using System.Reflection;
using VowelBonus.Application.Auth;

namespace VowelBonus.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(ISender sender) : base(sender)
        {
        }
        [HttpPost("Login", Name = "Login")]
        public async Task<Response<UserDto>> Login(LoginDto loginDto)
        {
            return await _sender.Send(new LoginCommand(loginDto));
        }
    }
}   

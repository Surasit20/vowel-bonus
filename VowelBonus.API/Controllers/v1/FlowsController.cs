using VowelBonus.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Controllers.v1;
using System.Reflection;
using VowelBonus.Application.Users;
using VowelBonus.Application.v1.Users;

namespace VowelBonus.API.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowsController : BaseController
    {
        public FlowsController(ISender sender) : base(sender)
        {
        }
        [HttpPost("Login", Name = "Login")]
        public async Task<Response<UserDto>> Login(LoginDto login)
        {
            return await _sender.Send(new LoginCommand(login));
        }
    }
}   

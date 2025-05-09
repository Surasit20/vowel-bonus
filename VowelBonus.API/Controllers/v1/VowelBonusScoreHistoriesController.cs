using VowelBonus.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Controllers.v1;
using System.Reflection;
using VowelBonus.Application.Users;
using VowelBonus.Application.v1.Users;
using VowelBonus.Application.v1.VowelBonusScoreHistories;

namespace VowelBonus.API.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class VowelBonusScoreHistoriesController : BaseController
    {
        public VowelBonusScoreHistoriesController(ISender sender) : base(sender)
        {
        }

        [HttpPost("CreateVowelBonusScoreHistories", Name = "CreateVowelBonusScoreHistories")]
        public async Task<Response<VowelBonusScoreHistoryDto>> CreateVowelBonusScoreHistories([FromBody] VowelBonusScoreHistorySaveDto args)
        {
            return await _sender.Send(new CreateVowelBonusScoreHistoryCommand(args));
        }
    }
}   

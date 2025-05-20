using VowelBonus.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Controllers.v1;
using System.Reflection;
using VowelBonus.Application.v1.VowelBonusScoreHistories;
using VowelBonus.Application.v1.Users;

namespace VowelBonus.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

        [HttpPost("GetVowelBonusScoreHistoriesByFilter", Name = "GetVowelBonusScoreHistoriesByFilter")]
        public async Task<Response<IEnumerable<VowelBonusScoreHistoryDto>>> GetVowelBonusScoreHistoriesByFilter([FromBody] VowelBonusScoreHistoryFilterDto args)
        {
            return await _sender.Send(new GetVowelBonusScoreHistoriesByFilterQuery(args));
        }

        [HttpPatch("UpdateVowelBonusScoreHistory", Name = "UpdateVowelBonusScoreHistory")]
        public async Task<Response<VowelBonusScoreHistoryDto>> UpdateVowelBonusScoreHistory([FromBody] VowelBonusScoreHistoryUpdateDto args)
        {
            return await _sender.Send(new UpdateVowelBonusScoreHistoryCommand(args));
        }

        [HttpDelete("DeleteVowelBonusScoreHistory", Name = "DeleteVowelBonusScoreHistory")]
        public async Task<Response<bool>> DeleteVowelBonusScoreHistory([FromQuery] int Id)
        {
            return await _sender.Send(new DeleteVowelBonusScoreHistoryCommand(Id));
        }

        [HttpGet("GetVowelBonusTotalScoreHistory", Name = "GetVowelBonusTotalScoreHistory")]
        public async Task<Response<int>> GetVowelBonusTotalScoreHistory([FromQuery] int Id)
        {
            return await _sender.Send(new GetVowelBonusTotalScoreHistoriesByUserIdQuery(Id));
        }
    }
}   

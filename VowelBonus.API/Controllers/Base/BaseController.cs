using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text.Json;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace POS.Controllers.v1
{
    public class BaseController : ControllerBase
    {
        protected readonly ISender _sender;

        public BaseController(ISender sender)
        {
            this._sender = sender;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class CallBackController : BaseApiController
    {
        private readonly ICallbackUrlService _callbackUrlService;
        private readonly ILogger _logger;
        public CallBackController(ICallbackUrlService callBackUrlService , ILogger<CallBackController> logger)
        {
            _callbackUrlService = callBackUrlService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCallBackUrlAsync([FromBody] CallBackUrl model)
        {
            _logger.LogInformation("callBack_API-starts");
            _logger.LogInformation(model.ToString());
            _logger.LogInformation(model.email);
            var response = await _callbackUrlService.CreateCallBackUrl(model);

            if (response.Status)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}

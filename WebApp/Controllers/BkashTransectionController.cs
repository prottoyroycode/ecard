using Library.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.bKash;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BkashTransectionController : ControllerBase
    {
        private IbkashTransectionService _bkashTransection;

        public BkashTransectionController(IbkashTransectionService  bkashTransection)
        {
            _bkashTransection = bkashTransection;
        }


        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetAsync(string paymentId)
        {

            var data = await _bkashTransection.CheckPaymentStatus(paymentId);
            return Ok(data);
        }


        [HttpDelete("CancelAgreement/{paymentId}")]
        public async Task<IActionResult> CancelAgreement(string paymentID)
        {

            var agreement = await _bkashTransection.GetAgreementFromDB(paymentID);

            if (agreement.Status == true)
            {
                var data = (bKashExecAgreementResponse)agreement.Data;

                var cancel = await _bkashTransection.BkashCancelAgreement(data.agreementID);
                return Ok(cancel);
            }
            return Ok(new Response { Data = null, Message = "Empty", Status = false, StatusCode = ResStatusCode.BadRequest });
        }
       


        //public async Task<AcceptedResult> CreateAgreement()
        //{
        //    var data = await _bkashTransection.CreateAgreement();
        //}
    }
}

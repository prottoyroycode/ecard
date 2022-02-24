using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.bKash;
using Models.DataContext;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.RockVilleResponse;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BkashCallBackController : ControllerBase
    {
        private IbkashTransectionService _bKashPayment;
        private readonly IbKashCallbackServices _callBack;
        private readonly EfDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IRockvillService _rockvillService;
        

        public BkashCallBackController(IbkashTransectionService bKashPayment , IbKashCallbackServices callBack , EfDbContext context, IEmailService emailService,
            IRockvillService rockvillService)
        {
            _bKashPayment = bKashPayment;
            _callBack = callBack;
            _context = context;
            _emailService = emailService;
            _rockvillService = rockvillService;

        }

        [HttpGet("transection")]
        public async Task<IActionResult> GetCallBack(string paymentID, string status, string? apiVersion)
        {
           
            var agreement = await _bKashPayment.ExecuteAgreement(paymentID);

            if (status == "cancel")
                
            {
                var err = (bKashAgreementException)agreement.Data;
                return Redirect("https://evouchers.store/checkout/successful/false/" + paymentID + "/" + err.statusMessage);
            }
   
           else if (status == "success")
            {
                //var datax = await _callBack.SaveCallbackAndUpdateOrderAsync(new bKashCallback(paymentID, status, apiVersion));
                return Redirect("https://evouchers.store/checkout/successful/true/"+paymentID+" ");

  
            }
            else
            {
                
                var err = (bKashAgreementException)agreement.Data;
                return Redirect("https://evouchers.store/checkout/successful/false/" + paymentID + "/"+err.statusMessage);

            }

            //return Ok(data);
        }
    }
}

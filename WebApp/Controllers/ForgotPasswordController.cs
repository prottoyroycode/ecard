using Library.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.ForgotPasswordViewModels;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ForgotPasswordController : BaseApiController
    {
        private readonly IForgotPassword _forgotPasswordService;
        public ForgotPasswordController(IForgotPassword forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;

        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassUserRequestVM forgotPassUserRequestVM)
        {

            if (forgotPassUserRequestVM.Email == "" || forgotPassUserRequestVM.Email is null)
                return Ok(new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.BadRequest,
                    Message = "please provide an email address",
                    Data = new { }

                });
            Random random = new Random();
            var value = random.Next(100001, 999999);
            var request = new ForgotPasswordRequestVM();
            request.Email = forgotPassUserRequestVM.Email;
            request.Otp = value;
            var result = await _forgotPasswordService.ForgotPassword(request);
            var userPassResetData = new UserPasswordResetCode();
            userPassResetData.Email = request.Email;
            userPassResetData.Code = value.ToString();

            if (result.Status == true)
            {
                var userResetPassData = await _forgotPasswordService.InserUserPasswordResetCode(userPassResetData);
            }

            return Ok(result);
        }
    }
}

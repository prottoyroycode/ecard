using Google.Apis.Auth;
using Library.Core.Enums;
using Library.Core.Infrastructure;
using Library.Core.Properties;
using Library.Core.Response;
using Library.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataContext;
using Models.Entities;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Version_1.Securites;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Securities;
using ViewModels.ForgotPasswordViewModels;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors]
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IForgotPassword _forgotPasswordService;

        public AccountController(IUserService userService, IJwtAuthManager jwtAuthManager, IForgotPassword forgotPasswordService)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
            _forgotPasswordService = forgotPasswordService;
        }
        //prottoy
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVM resetPasswordVM)
        {
            
           
            var result = await _forgotPasswordService.ResetPasswordAsync(resetPasswordVM);
            return Ok(result);
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
        //prottoy

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
        {
            CommonResponse response = new CommonResponse();
            string token = string.Empty;
            dynamic obj = new ExpandoObject();

            try
            {
                if (string.IsNullOrEmpty(model.RegisterWith) || model.RegisterWith != "E")
                    throw new Exception(Resources.InvalidAttempted);
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(s => s.Errors).Select(t => t.ErrorMessage).ToList();
                    throw new Exception(string.Join("; ", errors));
                }

                var result = await _userService.V1_UserRegistrationAsync(model);

                if (result.Item1)
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.Item3.Id.ToString()),
                        new Claim(ClaimTypes.Name,  result.Item3.FullName),
                        new Claim(ClaimTypes.Email,  result.Item3.Email),
                        new Claim(ClaimTypes.MobilePhone,  result.Item3.Mobile),
                        new Claim(ClaimTypes.Role, "userrole")
                    };
                    var jwtResult = _jwtAuthManager.GenerateTokens(result.Item3.Id.ToString(), claims);
                    obj.UserId = result.Item3.Id;
                    obj.FirstName = result.Item3.FullName;
                    obj.email = result.Item3.Email;
                    obj.mobile = result.Item3.Mobile;
                    obj.Gender = result.Item3.Gender;
                    obj.imageUrl = result.Item3.ImageUrl;
                    obj.AccessToken = jwtResult.AccessToken;
                    obj.RefreshToken = jwtResult.RefreshToken.TokenString;
                }
                response.Status = result.Item1;
                response.StatusCode = result.Item1 ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
                response.Message = result.Item2;
                response.Data = result.Item1 ? obj : new object { };

                if (result.Item1)
                    return Ok(await Task.FromResult(response));
                else
                    return BadRequest(await Task.FromResult(response));
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = ex.Message;
                return BadRequest(await Task.FromResult(response));
            }

        }

        [HttpPost("Login_V1"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            CommonResponse response = new CommonResponse();
            dynamic obj = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(s => s.Errors).Select(t => t.ErrorMessage).ToList();
                    throw new Exception(string.Join("; ", errors));
                }
                // validate social user
                if (model.RegisterWith == Provider.Facebook || model.RegisterWith == Provider.Google || model.RegisterWith == Provider.Linkedin || model.RegisterWith == Provider.Twitter)
                {
                    var data = await ValidateSocialUser(model);
                }

                var result = await _userService.V1_UserLoginAsync(model);

                if (result.Item1 == 1)
                {
                    List<string> registerList = new List<string>();

                    if (!string.IsNullOrEmpty(result.Item3.Email)) registerList.Add("E");

                    if (!string.IsNullOrEmpty(result.Item3.FacebookId)) registerList.Add("F");

                    if (!string.IsNullOrEmpty(result.Item3.GoogleId)) registerList.Add("G");

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.Item3.Id.ToString()),
                        new Claim(ClaimTypes.Name,  result.Item3.FullName),
                        new Claim(ClaimTypes.Email,  result.Item3.Email),
                        //new Claim(ClaimTypes.Role, "userrole")
                    };
                    var jwtResult = _jwtAuthManager.GenerateTokens(result.Item3.Id.ToString(), claims);
                    obj.UserId = result.Item3.Id;
                    obj.FirstName = result.Item3.FullName;
                    obj.email = result.Item3.Email;
                    obj.mobile = result.Item3.Mobile;
                    obj.Gender = result.Item3.Gender;
                    obj.imageUrl = result.Item3.ImageUrl;
                    obj.AccessToken = jwtResult.AccessToken;
                    obj.RefreshToken = jwtResult.RefreshToken.TokenString;
                    obj.RegisterWith = registerList.ToArray();

                }

                response.Status = result.Item1 == 1;
                response.StatusCode = result.Item1 == 1 ? (int)HttpStatusCode.OK : (result.Item1 == 0 ? (int)HttpStatusCode.BadRequest : (int)ResStatusCode.ExistBySocial);
                response.Message = result.Item2;
                response.Data = result.Item1 == 1 ? obj : new object { };

                if (result.Item1 == 1)
                    return Ok(await Task.FromResult(response));
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.ToString();
                return BadRequest(await Task.FromResult(response));
            }
        }


        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login_V2([FromBody] MsisdnLoginViewModel model)
        {
            CommonResponse response = new CommonResponse();
            dynamic obj = new ExpandoObject();

            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(s => s.Errors).Select(t => t.ErrorMessage).ToList();
                    throw new Exception(string.Join("; ", errors));
                }
                // validate social user
                

                var result = await _userService.V2_UserLoginAsync(model);

                if (result.Item1 == 1)
                {
                    List<string> registerList = new List<string>();

                  

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, result.Item3.Id.ToString()),
                        new Claim(ClaimTypes.Name,  result.Item3.FullName),
                        new Claim(ClaimTypes.Email,  result.Item3.Email),
                        //new Claim(ClaimTypes.Role, "userrole")
                    };
                    var jwtResult = _jwtAuthManager.GenerateTokens(result.Item3.Id.ToString(), claims);
                    obj.UserId = result.Item3.Id;
                    obj.FirstName = result.Item3.FullName;
                    obj.email = result.Item3.Email;
                    obj.mobile = result.Item3.Mobile;
                    obj.Gender = result.Item3.Gender;
                    obj.imageUrl = result.Item3.ImageUrl;
                    obj.AccessToken = jwtResult.AccessToken;
                    obj.RefreshToken = jwtResult.RefreshToken.TokenString;
                    obj.RegisterWith = registerList.ToArray();

                }

                response.Status = result.Item1 == 1;
                response.StatusCode = result.Item1 == 1 ? (int)HttpStatusCode.OK : (result.Item1 == 0 ? (int)HttpStatusCode.BadRequest : (int)ResStatusCode.ExistBySocial);
                response.Message = result.Item2;
                response.Data = result.Item1 == 1 ? obj : new object { };

                if (result.Item1 == 1)
                    return Ok(await Task.FromResult(response));
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.ToString();
                return BadRequest(await Task.FromResult(response));
            }
        }
        [HttpPost("[action]")]
        public ActionResult Logout()
        {
            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            return Ok();
        }

        #region privete method

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private async Task<bool> ValidateSocialUser(LoginViewModel viewModel)
        {
            bool result = false;
            if (viewModel.RegisterWith == Provider.Facebook)
            {
                // need authToken
                var path = "https://graph.facebook.com/me?access_token=" + viewModel.AccessToken;
                var client = new HttpClient();
                var uri = new Uri(path);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var validSocialUser = JsonConvert.DeserializeObject<ValidSocialUser>(await response.Content.ReadAsStringAsync());
                    if (validSocialUser.Id == viewModel.UserName) result = true;
                }
                else result = false;
            }
            else if (viewModel.RegisterWith == Provider.Google)
            {
                try
                {
                    // need idToken
                    GoogleJsonWebSignature.Payload googleResponse = await GoogleJsonWebSignature.ValidateAsync(viewModel.AccessToken);
                    if (googleResponse?.Subject == viewModel.UserName) result = true;
                    else result = false;
                }
                catch( Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }

        #endregion
    }

    public class ValidSocialUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Id_Str { get; set; }
    }

}
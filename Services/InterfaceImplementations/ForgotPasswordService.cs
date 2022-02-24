using Library.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.DataContext;
using Models.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ForgotPasswordViewModels;

namespace Services.InterfaceImplementations
{
    public class ForgotPasswordService : IForgotPassword 
    {
        private readonly EfDbContext _context;
        private readonly IEmailService _emailService;
        public ForgotPasswordService(EfDbContext context , IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<Response> ForgotPassword(ForgotPasswordRequestVM model )
        {

            string message="";
            string origin = "https://evouchers.store/verify-email";
            
            var response = new Response();
            var accountHolder =await _context.User.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (accountHolder == null)
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.NotFound,
                    Message="No user with this email is available",
                    Data = new {}

                };
           if(accountHolder.IsActive==false)
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.NotFound,
                    Message = "sorry this user is not an active user",
                    Data = new { }

                };
            if (string.IsNullOrEmpty(message))
            {
                var verifyUrl = $"{origin}?link={model.Otp}";
                message = $@"<p>Thank you for your request. Please click on the below link to reset your password:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
                // message = $@"<p>Please use this one time password: {model.Otp} to reset your password</p>";

            }

            _emailService.Send(
                to: accountHolder.Email,
                subject: "Password reset",
                html: $@"<h4>E-Card</h4>
                         {message}"
            );
            return new Response { 
            Status=true,
            StatusCode =ResStatusCode.Success,
            Message="an email with one time password has been sent to your address ,please check your inbox or spam folder",
                Data = new
                {
                    email =accountHolder.Email
                    

                }
            };

        }

        public async Task<Response> InserUserPasswordResetCode(UserPasswordResetCode userPasswordResetCode)
        {
            var response = new Response();
            try
            {
                await _context.UserPasswordResetCodes.AddAsync(userPasswordResetCode);
                var saveToDb = await _context.SaveChangesAsync();
                if(saveToDb > 0)
                {
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Created;
                    response.Message = "inserted Successfully";
                    response.Data = userPasswordResetCode;
                }
                
                

            }
            catch(Exception ex)
            {
                var messages = new List<string>();
                do
                {
                    messages.Add(ex.Message);
                    ex = ex.InnerException;
                }

                while (ex != null);
                response.Status = false;
                response.StatusCode = ResStatusCode.BadRequest;
                response.Message = "failed to insert data";
                response.Data = string.Join("",messages);
                

               // return new Response() { Status = false, Message = "Bad Request", Data = string.Join("", messages) };
            }
            return response;
        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            // var response = new Response();
            if (resetPasswordVM.Password != resetPasswordVM.ConfirmPassword)
            {
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.BadRequest,
                    Message = "password and confirm password do not match",
                    Data = new
                    {

                    }
                };
            }
            if (resetPasswordVM.Otp ==null || resetPasswordVM.Otp =="")
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.NotFound,
                    Message = "please provide your one time password",
                    Data = new { }

                };
            var checkUserByOtp = await _context.UserPasswordResetCodes.SingleOrDefaultAsync(u => u.Code == resetPasswordVM.Otp);
            if (checkUserByOtp == null)
            {
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.BadRequest,
                    Message = "otp did not match",
                    Data = new { }

                };
            }

            var CheckOtoExpiration = IsOtpExpired(resetPasswordVM.Otp);
            if(CheckOtoExpiration)
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.NotFound,
                    Message = "time expired",
                    Data = new { }

                };
            
            if(checkUserByOtp.IsUsed==false)
            {
                
                var changePassword = await _context.User.SingleOrDefaultAsync(u =>u.Email ==checkUserByOtp.Email);
                changePassword.Password = resetPasswordVM.Password;
                _context.User.Update(changePassword);
                var isUpdated =_context.SaveChanges();
                if(isUpdated>0)
                {
                    // var userResetTable = new UserPasswordResetCode();
                    checkUserByOtp.IsUsed = true;
                    //userResetTable.IsUsed = true;
                    _context.UserPasswordResetCodes.Update(checkUserByOtp);
                    _context.SaveChanges();
                }
                return new Response
                {
                    Status = true,
                    StatusCode = ResStatusCode.Success,
                    Message = "reset password done",
                    Data = new { }

                };

            }
            
                return new Response
                {
                    Status = false,
                    StatusCode = ResStatusCode.NotFound,
                    Message = "otp is already used",
                    Data = new { }

                };
            
            


        }
        public  bool IsOtpExpired(string otp)
        {
            var createdDate =  _context.UserPasswordResetCodes.SingleOrDefault(u => u.Code == otp);
            var createdDateTime = createdDate.CreatedDate;
            var requestdateTime = DateTime.Now;
            var difference = requestdateTime - createdDateTime;
            var minute = difference.TotalMinutes;
            if(minute<=5)
            return false;
            return true;
        }
    }
}

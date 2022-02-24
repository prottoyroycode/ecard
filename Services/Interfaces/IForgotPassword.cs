using Library.Core.ViewModels;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ForgotPasswordViewModels;

namespace Services.Interfaces
{
    public interface IForgotPassword
    {
        Task<Response> ForgotPassword(ForgotPasswordRequestVM model);
        Task<Response> InserUserPasswordResetCode(UserPasswordResetCode userPasswordResetCode);
        Task<Response> ResetPasswordAsync(ResetPasswordVM resetPasswordVM);
    }
}

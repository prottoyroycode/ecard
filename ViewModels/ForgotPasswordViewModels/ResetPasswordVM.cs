using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.ForgotPasswordViewModels
{
    public class ResetPasswordVM
    {
        public string Otp { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

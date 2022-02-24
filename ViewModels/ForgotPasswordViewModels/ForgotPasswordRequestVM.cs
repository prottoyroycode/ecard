using Library.Core.CustomDataAnnotation;
using Library.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.ForgotPasswordViewModels
{
    public class ForgotPasswordRequestVM
    {
        [CustomRegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z_]+?\.)([a-zA-Z]{2,3})(\]?)$", "RegisterWith", Provider.Email, ErrorMessage = "Please enter valid email address.")]
        public string Email { get; set; }
        public int Otp { get; set; }
    }
}

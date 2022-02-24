using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.ForgotPasswordViewModels
{
    public class UserPasswordResetCodeVM
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}

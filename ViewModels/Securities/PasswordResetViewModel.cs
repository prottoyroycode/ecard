using Library.Core.CustomDataAnnotation;
using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Securities
{
    public class PasswordResetViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required."), StringLength(10, MinimumLength = 8, ErrorMessage = "Password cannot be longer than 10 characters and less than 8 characters.")]
        public string Password { get; set; }
        public string Otp { get; set; }
    }
}

using Library.Core.CustomDataAnnotation;
using Library.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Securities
{
    public class RegistrationViewModel
    {
        public RegistrationViewModel()
        {
            FullName = "";
            Gender = "";
            FcmDeviceId = "";
            Country = "";
            CountryCode = "";
            City = "";
        }

        [CustomRequired("UserName", "", ErrorMessage = "UserId is required.")]
        //[CustomRequired("RegisterWith", Provider.Mobile, ErrorMessage = "Please enter valid Phone number.")]

        //[CustomRegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z_]+?\.)([a-zA-Z]{2,3})(\]?)$", "RegisterWith", Provider.Email, ErrorMessage = "Please enter valid email address.")]
        public string UserName { get; set; }

        [CustomRequired("RegisterWith", Provider.Email, ErrorMessage = "Password is required."), StringLength(10, MinimumLength = 8, ErrorMessage = "Password cannot be longer than 10 characters and less than 8 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Provider is required filed.")]
        public string RegisterWith { get; set; }

        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public string LinkedinId { get; set; }
        public string TwitterId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string FcmDeviceId { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string OAuthCode { get; set; }

    }
}

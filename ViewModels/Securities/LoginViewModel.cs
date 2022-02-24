using Library.Core.CustomDataAnnotation;
using Library.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Securities
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            FullName = "";
            Gender = "";
            FcmDeviceId = "";
            Country = "";
            CountryCode = "";
            City = "";
        }

        [CustomRequired("UserName", "", ErrorMessage = "UserId is required.")]
        [CustomRequired("RegisterWith", Provider.Email, ErrorMessage = "Please enter valid email address.")]

        [CustomRegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z_]+?\.)([a-zA-Z]{2,3})(\]?)$", "RegisterWith", Provider.Email, ErrorMessage = "Please enter valid email address.")]
        public string UserName { get; set; }

        [CustomRequired("RegisterWith", Provider.Email, ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [CustomRequired("RegisterWith", Provider.Facebook, ErrorMessage = "Access token is required.")]
        [CustomRequired("RegisterWith", Provider.Google, ErrorMessage = "Access token is required.")]
        [CustomRequired("RegisterWith", Provider.Linkedin, ErrorMessage = "Access token is required.")]
        [CustomRequired("RegisterWith", Provider.Twitter, ErrorMessage = "Access token is required.")]
        public string AccessToken { get; set; }

        [CustomRequired("RegisterWith", Provider.Twitter, ErrorMessage = "Secret key is required.")]
        public string SecretKey { get; set; }

        [Required(ErrorMessage = "Provider some required filed.")]
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
        public string ImageUrl { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string OAuthCode { get; set; }
    }
}

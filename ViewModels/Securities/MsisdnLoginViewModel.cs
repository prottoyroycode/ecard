using Library.Core.CustomDataAnnotation;
using Library.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Securities
{
    public class MsisdnLoginViewModel
    {
        public MsisdnLoginViewModel(string mobile, string password)
        {
            this.Mobile = mobile;
            this.Password = password;
        }


        [Required]
        public string Password { get; set; }




        [Required]
        public string Mobile { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using Models.Securities;
using System;

namespace ViewModel.Securities
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string BirthDate { get; set; }

        public IFormFile ImageFile { get; set; }

    }
}

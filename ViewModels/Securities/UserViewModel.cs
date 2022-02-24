using System;

namespace ViewModel.Securities
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ImageUrl { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string City { get; set; }

        public string RegisterWith { get; set; }

        public bool IsActive { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public string LinkedinId { get; set; }
        public string TwitterId { get; set; }

    }
}

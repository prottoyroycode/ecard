using Library.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Securities
{
    public class User : BaseModel
    {
        public User() : base()
        {
            //UserFcmDeviceHistory = new List<UserFcmDeviceHistory>();
            //UserLoginHistry = new List<UserLoginHistory>();
            //UserPasswordHistory = new List<UserPasswordHistory>();
        }

        [Key, Column(TypeName = "uniqueidentifier"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

      //  [Column(TypeName = "varchar"), StringLength(30)]
        public string UserName { get; set; }

       // [Column(TypeName = "nvarchar"), StringLength(50)]
        public string FullName { get; set; }

       // [Column(TypeName = "varchar"), StringLength(50)]
        public string Mobile { get; set; }

      //  [Column(TypeName = "varchar"), StringLength(20)]
        public string Email { get; set; }

      //  [Column(TypeName = "varchar"), StringLength(20)]
        public string FacebookId { get; set; }

        [Column(TypeName = "varchar"), StringLength(50)]
        public string GoogleId { get; set; }

        [Column(TypeName = "varchar"), StringLength(50)]
        public string LinkedinId { get; set; }

        [Column(TypeName = "varchar"), StringLength(50)]
        public string TwitterId { get; set; }

        [Column(TypeName = "varchar"), StringLength(300)]
        public string Password { get; set; }

        [Column(TypeName = "varchar"), StringLength(300)]
        public string ImageUrl { get; set; }

        [Column(TypeName = "varchar"), StringLength(100)]
        public string ImageName { get; set; }

        [Column(TypeName = "varchar"), StringLength(10)]
        public string ImageSource { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }

        [Column(TypeName = "varchar"), StringLength(10)]
        public string Gender { get; set; } 

        [Column(TypeName = "varchar"), StringLength(20)]
        public string Country { get; set; }

        [Column(TypeName = "varchar"), StringLength(5)]
        public string CountryCode { get; set; }

        [Column(TypeName = "varchar"), StringLength(50)]
        public string City { get; set; }    

        [Column(TypeName = "varchar"), StringLength(2)]
        public string RegisterWith { get; set; }

        [Column(TypeName = "bit")]
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "varchar"), StringLength(50)]
        public string RefreshToken { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<UserFcmDeviceHistory> UserFcmDeviceHistory { get; set; }
        public virtual ICollection<UserLoginHistory> UserLoginHistry { get; set; }
        public virtual ICollection<UserPasswordHistory> UserPasswordHistory { get; set; }
    }
}
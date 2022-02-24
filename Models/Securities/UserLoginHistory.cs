using Library.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Securities
{
    public class UserLoginHistory : BaseModel
    {
        [Required, Column(TypeName = "int"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column(TypeName = "uniqueidentifier")]
        public Guid UserId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LogInTIme { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        public DateTime? LogOutTIme { get; set; }

        [Required, Column(TypeName = "varchar"), StringLength(1000)]
        public string FcmDeviceId { get; set; }

        [NotMapped]
        public virtual User User { get; set; }
    }
}
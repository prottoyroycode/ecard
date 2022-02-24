using Library.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Securities
{
    public class UserPasswordHistory : BaseModel
    {
        [Key, Required, Column(TypeName = "int", Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key, Required, Column(TypeName = "uniqueidentifier", Order = 1)]
        public Guid UserId { get; set; }

        [Key, Required, Column(TypeName = "varchar", Order = 2), StringLength(15)]
        public string Password { get; set; }

        [NotMapped]
        public virtual User User { get; set; }
    }
}

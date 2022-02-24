using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class UserPasswordResetCode
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

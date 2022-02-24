using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
    public class EmailSMTPConfiguration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string WebDomain { get; set; }
        public bool IsSSL { get; set; }
    }
}

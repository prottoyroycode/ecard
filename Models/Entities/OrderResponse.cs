using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
    public class OrderResponse : AuditTable
    {
        public string success { get; set; }
        public string responseCode { get; set; }
        public string description { get; set; }
        [NotMapped]
        public object data { get; set; }
        public string dataStore { get; set; }
        public Guid reference_code { get; set; }

    }
}

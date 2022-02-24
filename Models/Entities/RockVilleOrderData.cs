using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
    [Table("RockVilleOrderData")]
    public class RockVilleOrderData :AuditTable
    {

        public string sku { get; set; }
        public int quantity { get; set; }
        public int pre_order { get; set; } = 0;
        public string email { get; set; }
        public decimal price { get; set; }
        public string store_id { get; set; }
        public Guid reference_code { get; set; }

    }
}

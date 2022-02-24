using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
   public  class ECardVoucher:AuditTable
    {
    
        public string title { get; set; }
        public string sku { get; set; }
        public decimal min_price { get; set; }
        public string min_price_str { get; set; }
        public string image { get; set; }
        public string image2 { get; set; }
        public string description { get; set; }
        public string card_type { get; set; }
        public string main_max_price { get; set; }
        public string main_min_price { get; set; }

        [NotMapped]
        public  List<string> favorites { get; set; }


    }
}

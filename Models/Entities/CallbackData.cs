using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class CallbackData
    {
        public int Id { get; set; }
        public bool success { get; set; } = true;
        public int responseCode { get; set; } = 0;
        public string description = "success";
        public int order_id { get; set; }
        public string email { get; set; }
        public int status { get; set; }
        public string status_text { get; set; }
        public DateTime created_time { get; set; }
        public string reference_code { get; set; }
        public int count { get; set; }
        public double unit_price { get; set; }
        public bool is_completed { get; set; }
        public int sku { get; set; }
        public string title { get; set; }
        public int CardCount { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public string CardResults { get; set; }
        //  public string card_number { get; set; }
        // public string pin_code { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVilleResponse
{
    public class CallBackModel
    {
        public CallBackModel()
        {
            order = new OrderCB();
            cards = new Cards();
        }
        public bool success { get; set; } = true;
        public int responseCode { get; set; } = 0;
        public string description = "success";
        public OrderCB order { get; set; }
        public Cards cards { get; set; }

    }
    public class OrderCB
    {
        public OrderCB()
        {
            product = new Product();
        }
        public int order_id { get; set; }
        public string email { get; set; }
        public int status { get; set; }
        public string status_text { get; set;}
        public DateTime created_time { get; set; }
        public string reference_code { get; set; }


        public Product product { get; set; }
        public int count { get; set; }
        public double unit_price { get; set; }
        public bool is_completed { get; set; }

    }
    public class Product
    {
        public int sku { get; set; }
        public string title { get; set; }

    }
    public class Cards
    {
        public Cards()
        {
            results = new List<Results>();
        }
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Results> results { get; set; }


    }
    public class Results
    {
        public string card_number { get; set; }
        public string pin_code { get; set; }
    }
}

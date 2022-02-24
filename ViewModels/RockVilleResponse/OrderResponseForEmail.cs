using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVilleResponse
{
    public class OrderResponseForEmail
    {
        public string productName { get; set; }
        public decimal price { get; set; }
        public string orderId { get; set; }
    }
}

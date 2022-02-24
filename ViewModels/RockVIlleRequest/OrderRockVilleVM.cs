using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVIlleRequest
{
    public class OrderRockVilleVM
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public int Pre_order { get; set; } = 0;
        public string Email { get; set; }
        public decimal Price { get; set; }
        public int Store_Id { get; set; }
        public Guid Reference_code { get; set; } 
    }
    //public class OrderTestRockVilleVM
    //{
    //    public string sku { get; set; }
    //    public int quantity { get; set; }
    //    public int pre_order { get; set; } = 0;
    //    public string email { get; set; }
    //    public decimal price { get; set; }
    //    public string store_id { get; set; }
    //    public Guid reference_code { get; set; }
    //}
    public class OrderTestRockVilleVM
    {
        public string sku { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string destination { get; set; }
        public int delivery_type { get; set; }
        public int pre_order { get; set; } = 0;

        public Guid reference_code { get; set; }
        public int terminal_id { get; set; }
        public int terminal_pin { get; set; }
    }
}

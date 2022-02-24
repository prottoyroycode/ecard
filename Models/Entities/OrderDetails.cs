using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entities
{
    public class OrderDetails
    {
        //[Key]
        public Guid OrderDetailsId { get; set; }
        public string Sku { get; set; }
        public string StoreId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrlPath { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public Order order { get; set; } 

    }
}

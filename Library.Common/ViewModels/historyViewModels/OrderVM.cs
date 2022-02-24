using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ViewModels.historyViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {
            OrderDetails = new List<OrderDetailsVM>();
        }
        public DateTime CreatedOn { get; set; }
        public decimal OrderAmount { get; set; }
        public string PaymentID { get; set; }
        public List<OrderDetailsVM> OrderDetails { get; set; }
    }
    public class OrderDetailsVM
    {
        public string Sku { get; set; }
        public string StoreId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrlPath { get; set; }
        public string Title { get; set; }
    }
}

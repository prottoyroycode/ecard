using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.OrderViewModels
{
    public class OrderVM :Order
    {

        public OrderVM(Order order)
        {
            this.Id = order.Id;
            this.OrderAmount = order.OrderAmount;
            this.UserId = order.UserId;
            this.OrderDetails = order.OrderDetails;

        }
  
      public string bKashUrl { get; set; }
    }
    public class CatalogAvailabilityVM
    {
        public string Sku { get; set; }
        public int Item_Count { get; set; }
        public decimal Order_Price { get; set; }
    }
    //public class OrderDetailsVM:OrderDetails
    //{

    //}
}

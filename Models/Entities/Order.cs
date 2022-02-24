using Library.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities
{
    public class Order:AuditTable
    {
        public Order()
        {
            this.OrderDetails = new List<OrderDetails>();
            this.Status = OrderStatus.Initialize;
            this.Channel = OrderChannel.Web;
            this.Payment = OrderPayment.bkash;
            this.Delivery = Orderdelivery.email;
            this.OrderAmount = 0;
            this.PreOrder = "0";
        }
        //[Key]
      //  public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PreOrder { get; set; }
        public decimal OrderAmount { get; set; }
        public string Status { get; set; } 
        [Required]
        public string Channel { get; set; }
       
        public string Payment { get; set; } 
        public string Delivery { get; set; }
        public string Email { get; set; } = "";
        public string WhatsApp { get; set; } = "";
        public IList<OrderDetails> OrderDetails { get; set; }
    }
    
}

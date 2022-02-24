using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Favourite:AuditTable
    {
       
        public Guid UserId { get; set; }
        public string Sku { get; set; }
        public string ImageUrlPath { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public bool IsFavourite { get; set; } = true;



    }
}

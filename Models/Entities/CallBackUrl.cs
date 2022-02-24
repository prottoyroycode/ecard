using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class CallBackUrl
    {
        public string referenceCode { get; set; }
        public long productId { get; set; }
        public string email { get; set; }
    }
}

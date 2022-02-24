using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVilleResponse
{
    public class GetOrderByRef
    {
        public bool success { get; set; }
        public int responseCode { get; set; }
        public string description { get; set; }
        public object data { get; set; }
    }

}

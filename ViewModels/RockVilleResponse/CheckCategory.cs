using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVilleResponse
{
    public class CheckCategory
    {
        public bool success { get; set; }
        public int responseCode { get; set; }
        public string description { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public bool availability { get; set; } = false;
        public string detail { get; set; }
    }

}

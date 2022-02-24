using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVilleResponse
{
    public class RockVilleCardResponse
    {
        public bool success { get; set; }
        public int responseCode { get; set; }
        public string description { get; set; }
        public DataObj data { get; set; }


    }
    public class DataObj
    {
        public string count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<ResultObj> results { get; set; } = new List<ResultObj>();

    }
    public class ResultObj
    {
        public string card_number { get; set; }
        public string pin_code { get; set; }
        public string claim_url { get; set; }
    }
}

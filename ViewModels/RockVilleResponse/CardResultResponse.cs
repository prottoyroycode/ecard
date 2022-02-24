using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.RockVilleResponse
{
    public class CardResultResponse
    {
        public string card_number { get; set; }
        public string pin_code { get; set; }
        public string claim_url { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
    public class bKashToken
    {
        public string id_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}

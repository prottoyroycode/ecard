﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.UserViewModels
{
  public  class RockVillTockenVM
    {
        
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string scope { get; set; }
        public string jti { get; set; }

    }
}

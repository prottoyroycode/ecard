using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.bKash
{
  public  class bKashCreateAgreementResponseVM
    {
        public bKashCreateAgreementResponseVM()
        {
            this.bkashURL = null;
            this.statusCode = 2003.ToString();
            this.statusMessage = "Process failed";
        }
        public string bkashURL { get; set; }
        
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}

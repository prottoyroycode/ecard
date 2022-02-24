using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
   public  class bKashCallback:AuditTable
    {

        public bKashCallback(string paymentID , string status ,string? apiVersion )
        {
            this.paymentID = paymentID;
            this.status = status;
            this.apiVersion = apiVersion;
        }
        public string paymentID { get; set; }
        public string status { get; set; }
        public string apiVersion { get; set; }
    }
}

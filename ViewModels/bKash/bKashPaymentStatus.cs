using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.bKash
{
  public  class bKashPaymentStatus
    {
        public string paymentID{get;set;}
        public string mode{get;set;}
        public string verificationStatus{get;set;}
        public string statusCode{get;set;}
        public string statusMessage{get;set;}
        public string payerReference{get;set;}
        public string agreementID{get;set;}
        public string agreementStatus{get;set;}
        public string agreementCreateTime{get;set;}
        public string agreementExecuteTime{get;set;}
    }
}

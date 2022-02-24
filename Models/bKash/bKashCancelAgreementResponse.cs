using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
   public class bKashCancelAgreementResponse: AuditTable
    {
        public string statusCode{get;set;}
        public string statusMessage{get;set;}
        public string paymentID{get;set;}
        public string agreementID{get;set;}
        public string payerReference{get;set;}
        public string agreementVoidTime{get;set;}
        public string agreementStatus{get;set;}
    }
}

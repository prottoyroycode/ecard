using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
   public class bKashExecAgreementResponse: AuditTable
    {
        public string paymentID{get;set;}
        public string agreementID{get;set;}
        public string customerMsisdn{get;set;}
        public string payerReference{get;set;}
        public string agreementExecuteTime{get;set;}
        public string agreementStatus{get;set;}
        public string paymentExecuteTime{get;set;}
        public string trxID{get;set;}
        public string transactionStatus{get;set;}
        public string amount{get;set;}
        public string currency{get;set;}
        public string intent{get;set;}
        public string merchantInvoiceNumber{get;set;}
        public string statusCode{get;set;}
        public string statusMessage{get;set;}
    }
}

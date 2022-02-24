using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
   public class bKashCreateAgreementRequest:AuditTable
    {
        public bKashCreateAgreementRequest(string amount , string merchantInvoiceNumber , string payerReference  , string mode )
        {
            this.mode = mode;

            this.payerReference = payerReference;
            this.callbackURL = AppConstants.PaymentCallback;
            this.amount = amount;
            this.merchantInvoiceNumber = merchantInvoiceNumber;
            this.agreementID = AppConstants.agreementID;
            this.intent = "sale";
            this.currency = "BDT";
        }


        public string mode { get; set; }
        public string payerReference { get; set; }
        public string callbackURL { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string agreementID { get; set; }
        public string merchantInvoiceNumber { get; set; }

    }
}

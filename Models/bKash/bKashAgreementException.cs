using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
  public class bKashAgreementException: AuditTable
    {
        public bKashAgreementException()
        {
            this.merchantInvoiceNumber = null;
            this.paymentID = null;
        }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string trxID { get; set; }
    }
}

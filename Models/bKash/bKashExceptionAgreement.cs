using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
   public class bKashExceptionAgreement:AuditTable
    {

        public string merchantInvoiceNumber { get; set; }
        public string errorString { get; set; }
    }
}

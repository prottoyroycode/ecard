using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
    public class bKashExecAgreementRequest: AuditTable
    {
      
        public string paymentID { get; set; }
    }
}

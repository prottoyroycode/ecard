using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.bKash
{
  public  class bKashCancelAgreementRequest: AuditTable
    {
        public string agreementId { get; set; }
    }
}

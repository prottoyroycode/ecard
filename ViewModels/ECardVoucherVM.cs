using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
  public  class ECardVoucherVM: ECardVoucher
    {
        public ECardVoucherVM()
        {
            this.favorites = new List<string>();
        }
        public virtual List<string> favorites { get; set; }
    }
}

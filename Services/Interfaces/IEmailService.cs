using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.RockVilleResponse;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
        Task SendInvoiceMail(string to, string subject,   CardResultResponse cardResultResponse ,OrderResponseForEmail orderResponseForEmail);
    }
}

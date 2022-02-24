using Library.Core.ViewModels;
using Models.bKash;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.bKash;

namespace Services.Interfaces
{
   public interface IbkashTransectionService
    {
       
        bKashToken GenerateToken();
        Task<bKashCreateAgreementResponseVM> CreateAgreement(string amount, string invoiceNumber , string payerReferance);
        Task<bKashCreateAgreementResponseVM> CreateAgreementWithAgreementId(string amount, string invoiceNumber , string payerReferance);
        Task<Response> ExecuteAgreement(string paymentID);
        Task<Response> CheckPaymentStatus(string paymentID);
        Task<Response> BkashCancelAgreement(string agreementId);
        Task<Response> BakshTransactionSearchByTranId(string transactionId);
        Task<Response> GetAgreementFromDB(string paymentID);
        Task<Response> CheckAgreementStatusByAgreementId(string agreementID);

    }
}

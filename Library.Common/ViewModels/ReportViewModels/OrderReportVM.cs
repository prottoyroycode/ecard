using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ViewModels.ReportViewModels
{
    public class OrderReportVM 
    {
        public OrderReportVM()
        {
         
            OrderDetailsReportVMs = new List<OrderDetailsReportVM>();
            
        }
        public DateTime CreatedOn { get; set; }
        public decimal OrderAmount { get; set; }
        public string PaymentID { get; set; } 
        public Guid Orderid { get; set; }
        
        public List<OrderDetailsReportVM> OrderDetailsReportVMs { get; set; }
        public bKashCreateAgreementRequestReportVM BKashCreateAgreementRequestReportVM { get; set; }
    }
    public class OrderDetailsReportVM
    {
        public string Sku { get; set; }
        public string StoreId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrlPath { get; set; }
        public string Title { get; set; }
    }
    public class bKashCreateAgreementRequestReportVM 
    {
        public string mode { get; set; }
        public string payerReference { get; set; }
        public string callbackURL { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string agreementID { get; set; }
        public string merchantInvoiceNumber { get; set; }
    }
    public class bKashCreateAgreementResponse
    {
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string paymentCreateTime { get; set; }
        public string transactionStatus { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string bkashURL { get; set; }
        public string callbackURL { get; set; }
        public string successCallbackURL { get; set; }
        public string failureCallbackURL { get; set; }
        public string cancelledCallbackURL { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
    public class bKashExecAgreementRequestReportVM
    {
        public string agreementId { get; set; }
    }
    public class bKashExecAgreementResponseReportVM
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string payerReference { get; set; }
        public string agreementVoidTime { get; set; }
        public string agreementStatus { get; set; }
    }
}


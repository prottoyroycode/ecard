using Library.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.bKash;
using Models.DataContext;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModels.bKash;

namespace Services.InterfaceImplementations
{
    public class bkashTransectionService : IbkashTransectionService
    {
        private bKashToken bKashToken;
        private readonly EfDbContext _context;
        public bkashTransectionService(EfDbContext context) 
        {
            bKashToken =  this.GenerateToken();
            _context = context;
        }
       





        public async Task<bKashCreateAgreementResponseVM> CreateAgreement(string amount , string invoiceNumber , string payerReferance)
         {
           
            bKashCreateAgreementResponseVM resultResponse;
            var url = AppConstants.CreateAgreementUrl;
                        
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;
            var requestData = new bKashCreateAgreementRequest(amount , invoiceNumber , payerReferance, "0000");
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                _context.bKashCreateAgreementRequests.Add(requestData);
                _context.SaveChanges();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = await streamReader.ReadToEndAsync();

                if (result.Contains("paymentID"))
                {
                    var resultData = JsonConvert.DeserializeObject<bKashCreateAgreementResponse>(result);

                    resultData.merchantInvoiceNumber = string.IsNullOrEmpty(resultData.merchantInvoiceNumber) ? invoiceNumber : resultData.merchantInvoiceNumber;
                    _context.bKashCreateAgreementResponses.Add(resultData);
                    _ = _context.SaveChangesAsync();
                    return  new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL , statusCode = resultData.statusCode , statusMessage = resultData.statusMessage};
                }
                if(!result.Contains("paymentID"))
                {
                    var resultData = JsonConvert.DeserializeObject<bKashCreateAgreementResponse>(result);
                    return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }
                
                
                else
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == Guid.Parse(invoiceNumber));
                   
                    // get the order and set status failed
                    var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(result);
                    order.Status = "Failed";
                    _context.Orders.Update(order);
                    _ = _context.SaveChangesAsync();

                    var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = invoiceNumber };
                    _ = _context.bKashAgreementExceptions.AddAsync(exception);
                    _ = _context.SaveChangesAsync();

                    return new bKashCreateAgreementResponseVM { bkashURL= null, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }

               
            }
            catch (Exception ex)
            {
                
                var aaa = ex.ToString();
              

                throw ex;
            }
            
            //return aData;

        }

        public async Task<bKashCreateAgreementResponseVM> CreateAgreementWithAgreementId(string amount, string invoiceNumber, string payerReferance )
        {
            bKashCreateAgreementResponseVM resultResponse;
            var url = AppConstants.CreateAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;
            var requestData = new bKashCreateAgreementRequest(amount, invoiceNumber, "01770618575", "0001");
            var data = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                _context.bKashCreateAgreementRequests.Add(requestData);
                _context.SaveChanges();
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = await streamReader.ReadToEndAsync();

                if (result.Contains("paymentID"))
                {
                    var resultData = JsonConvert.DeserializeObject<bKashCreateAgreementResponse>(result);

                    resultData.merchantInvoiceNumber = string.IsNullOrEmpty(resultData.merchantInvoiceNumber) ? invoiceNumber : resultData.merchantInvoiceNumber;
                    _context.bKashCreateAgreementResponses.Add(resultData);
                    _ = _context.SaveChangesAsync();
                    return new bKashCreateAgreementResponseVM { bkashURL = resultData.bkashURL, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }


                else
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == Guid.Parse(invoiceNumber));

                    // get the order and set status failed
                    var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(result);
                    order.Status = "Failed";
                    _context.Orders.Update(order);
                    _ = _context.SaveChangesAsync();

                    var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = invoiceNumber };
                    _ = _context.bKashAgreementExceptions.AddAsync(exception);
                    _ = _context.SaveChangesAsync();

                    return new bKashCreateAgreementResponseVM { bkashURL = null, statusCode = resultData.statusCode, statusMessage = resultData.statusMessage };
                }


            }
            catch (Exception ex)
            {
                var aaa = ex.ToString();
                throw;
            }

            //return aData;

        }
        public async Task<Response> ExecuteAgreement(string paymentID)
        {

            Response result = new Response { Message = "Failed", StatusCode = ResStatusCode.BadRequest, Status = false, Data = null };

            var url = AppConstants.ExexAgreementUrl;

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] =bKashToken.id_token ;
            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] =  AppConstants.tokenize_app_key ;

            var request = new bKashExecAgreementRequestVM { paymentID =  paymentID};

            var requestData = JsonConvert.SerializeObject(request);
            _ = await _context.bKashExecAgreementRequests.AddAsync(new bKashExecAgreementRequest { paymentID = request.paymentID});
            _ =  _context.SaveChangesAsync();
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(requestData);
            }
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
           
            var resultResponse = await streamReader.ReadToEndAsync();

            if (resultResponse.Contains("paymentID"))
            {
                result.Message = "Success";
                result.Status = true;
                result.StatusCode = ResStatusCode.Success;
                
                var successResponse = JsonConvert.DeserializeObject<bKashExecAgreementResponse>(resultResponse);
                _ = await _context.bKashExecAgreementResponses.AddAsync(successResponse);
                _ = _context.SaveChangesAsync();
                result.Data = successResponse;
                return result;
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(resultResponse);
                var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null , paymentID = paymentID };
                _ = _context.bKashAgreementExceptions.AddAsync(exception);
                _ = _context.SaveChangesAsync();

                result.Message = "Failed";
                result.Status = false;
                result.StatusCode = ResStatusCode.BadRequest;
                result.Data = exception;
            }
            return result;

        }



        public bKashToken GenerateToken()
        {
            try
            {
                
                var httpRequest = (HttpWebRequest)WebRequest.Create(AppConstants.TokenUrl);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers["password"] = AppConstants.tokenize_pass;
                httpRequest.Headers["username"] = AppConstants.tokenize_username;
                var tonekRequest = new TokenRequest(AppConstants.tokenize_app_secret, AppConstants.tokenize_app_key);
                var requestData = Newtonsoft.Json.JsonConvert.SerializeObject(tonekRequest);
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(requestData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result =  streamReader.ReadToEnd();


                if (result.Contains("token_type"))
                {
                    var resultData = JsonConvert.DeserializeObject<bKashToken>(result);

                    return resultData;
                }
                else if (result.Contains("fail"))
                {
                    return new bKashToken();
                }
                else
                {
                    return new bKashToken();
                }
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var result = streamReader.ReadToEnd();
                //}



            }
            catch (Exception exc)
            {


                return new bKashToken();
            }
        }

        public async Task<Response> CheckPaymentStatus(string paymentID)
        {
            var url = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/payment/status";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;

            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;

            var Obj = new { paymentID = paymentID };
            var data = JsonConvert.SerializeObject(Obj);

           

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();

            if (resultResponse.Contains("paymentID"))
            {
                var successResponse = JsonConvert.DeserializeObject<bKashPaymentStatus>(resultResponse);

                return new Response { Data = successResponse, Message = "Success", Status = true, StatusCode = ResStatusCode.Success };
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(resultResponse);
                var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, paymentID = paymentID };

                return new Response { Data = exception, Message = "Failed", Status = false, StatusCode = ResStatusCode.NotFound };
            }

          

        }

        public async Task<Response> BkashCancelAgreement(string agreementId)
        {
            var url = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/agreement/cancel";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;

            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;

            var Obj = new { agreementId = agreementId };
            var data = JsonConvert.SerializeObject(Obj);

            var request = new bKashCancelAgreementRequest();
            request.agreementId = agreementId;
            await _context.bKashCancelAgreementRequests.AddAsync(request);
            await _context.SaveChangesAsync();

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();
            if (resultResponse.Contains("agreementID"))
            {
                var successResponse = JsonConvert.DeserializeObject<bKashCancelAgreementResponse>(resultResponse);
                _ = _context.bKashCancelAgreementResponses.AddAsync(successResponse);
                _ = _context.SaveChangesAsync();
                return new Response { Data = successResponse, Message = "Success", Status = true, StatusCode = ResStatusCode.Success };
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(resultResponse);
                var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, agreementID = agreementId };

                return new Response { Data = exception, Message = "Failed", Status = false, StatusCode = ResStatusCode.NotFound };
            }
            
        }

        public async Task<Response> BakshTransactionSearchByTranId(string transactionId)
        {
            var url = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/general/searchTransaction";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;

            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;

            var Obj = new { trxID = transactionId };
            var data = JsonConvert.SerializeObject(Obj);



            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();
            if (resultResponse.Contains("trxID"))
            {
                var successResponse = JsonConvert.DeserializeObject<bKashPaymentStatus>(resultResponse);

                return new Response { Data = successResponse, Message = "Success", Status = true, StatusCode = ResStatusCode.Success };
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(resultResponse);
                var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, trxID = transactionId };

                return new Response { Data = exception, Message = "Failed", Status = false, StatusCode = ResStatusCode.NotFound };
            }
            
        }


        public async Task<Response> GetAgreementFromDB(string paymentID)
        {
            try
            {
                var agreement = await _context.bKashExecAgreementResponses.FirstOrDefaultAsync(a => a.paymentID == paymentID);

                if (agreement != null)
                {
                    return new Response { Data = agreement, Message = "Success", Status = true, StatusCode = ResStatusCode.Success, TotalRecords = 1 };
                }
                else
                {
                    return new Response { Data = null, Message = "Failed", Status = false, StatusCode = ResStatusCode.BadRequest, TotalRecords = 0 };
                }
            }
            catch (Exception ex)
            {
                return new Response { Data = null, Message = ex.ToString(), Status = false, StatusCode = ResStatusCode.BadRequest, TotalRecords = 0 };
                throw;
            }
        }

        public async Task<Response> CheckAgreementStatusByAgreementId(string agreementID)
        {
            var url = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/tokenized/checkout/agreement/status";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = bKashToken.id_token;

            httpRequest.ContentType = "application/json";
            httpRequest.Headers["X-APP-Key"] = AppConstants.tokenize_app_key;

            var Obj = new { agreementID = agreementID };
            var data = JsonConvert.SerializeObject(Obj);



            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var resultResponse = await streamReader.ReadToEndAsync();

            if (resultResponse.Contains("paymentID"))
            {
                var successResponse = JsonConvert.DeserializeObject<bKashPaymentStatus>(resultResponse);

                return new Response { Data = successResponse, Message = "Success", Status = true, StatusCode = ResStatusCode.Success };
            }
            else
            {
                var resultData = JsonConvert.DeserializeObject<bKashAgreementExceptionVM>(resultResponse);
                var exception = new bKashAgreementException { statusCode = resultData.statusCode, statusMessage = resultData.statusMessage, merchantInvoiceNumber = null, agreementID = agreementID };

                return new Response { Data = exception, Message = "Failed", Status = false, StatusCode = ResStatusCode.NotFound };
            }

        }
    }
}

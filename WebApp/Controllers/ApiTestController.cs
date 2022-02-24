using Library.Core.ViewModels.ReportViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DataContext;
using Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.RockVilleResponse;

namespace WebApi.Controllers
{
    public class CheckAgreementStatus
    {
        public string AgreementId { get; set; }
    }
    public class CreateAgreementrequest
    {
        public string Amount { get; set; } = "10";
        public string InvoiceNumber { get; set; } 
        public string PayerReference { get; set; } = "8801711507136";
    }
    public class OrderReport
    {
        public OrderReport()
        {
            OrderDetails = new List<OrderDetailsReport>();
        }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal OrderAmount { get; set; }
        public List<OrderDetailsReport> OrderDetails { get; set; }

    }
    public class OrderDetailsReport
    {
        public string Sku { get; set; }
        public string StoreId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrlPath { get; set; }
        public string Title { get; set; }
    }
    //public class OrderResponse
    //{
    //    public string productName { get; set; }
    //    public string price { get; set; }
    //    public string orderId { get; set; }
    //}

    public class ApiTestController:BaseApiController
    {
        private readonly ITokenFetch _tokenService;
        private readonly EfDbContext _context;
        private readonly IFavouriteService _favoriteService;
        private readonly IEmailService _emailService;
        private readonly IRockvillService _rockvillService;
        private readonly IbkashTransectionService _bkashTransectionService;
        public ApiTestController(ITokenFetch tokenService, EfDbContext context, IFavouriteService favoriteService, IEmailService emailService ,
            IRockvillService rockvillService, IbkashTransectionService bkashTransectionService)
        {
            _tokenService = tokenService;
            _context = context;
            _favoriteService = favoriteService;
            _emailService = emailService;
            _rockvillService = rockvillService;
            _bkashTransectionService = bkashTransectionService;
        }
        [HttpPost("bkashAgreementCancel")]
        public async Task<IActionResult>BkashAgreementCancel(string agreemntId)
        {
            var data = await _bkashTransectionService.BkashCancelAgreement(agreemntId);
            return Ok(data);
        }
        [HttpPost("searchBkashTransaction")]
        public async Task<IActionResult> SearchBkashTransaction(string transactionId)
        {
            var data = await _bkashTransectionService.BakshTransactionSearchByTranId(transactionId);
            return Ok(data);
        }
        [HttpPost("generateBkashToken")]
        public IActionResult GenerateTokenBkash()
        {
            var result = _bkashTransectionService.GenerateToken();
            return Ok(result);
        }
        [HttpPost("agremeentStatus")]
        public async Task<IActionResult>AgreementStatusCheck([FromBody] CheckAgreementStatus checkAgreementStatus)
        {
            var result = await _bkashTransectionService.CheckAgreementStatusByAgreementId(checkAgreementStatus.AgreementId);
            return Ok(result);
        }


        [HttpPost("createAgreement")]
        public async Task<IActionResult>CreateAgreementApi()
        {
            var random = new Random();
            int num = random.Next(1000);
            var agremeentrequest = new CreateAgreementrequest();
            agremeentrequest.InvoiceNumber = $"inv-{num} " ;
           // return Ok();
           var aggreementResponse = await _bkashTransectionService.CreateAgreement(agremeentrequest.Amount, agremeentrequest.InvoiceNumber, 
                agremeentrequest.PayerReference);
            return Ok(aggreementResponse.bkashURL);
        }


        [HttpGet("getCards")]
        public async Task<IActionResult>GetCardsByRefCode()
        {
            

            
            var ref_code = "15a2652c-8bf3-4830-b662-cc8d634aca2f";
            Guid ref_code1 = Guid.Parse(ref_code);
            var p = await _context.OrderResponses.FirstOrDefaultAsync(s => s.reference_code == ref_code1);
            var dataStore = p.dataStore;
            dynamic userObj = JObject.Parse(dataStore);
            var orderResponse = new OrderResponseForEmail();
            orderResponse.productName = userObj.product.title;
            orderResponse.price = userObj.total_face_value;
            orderResponse.orderId = userObj.order_id;

            var data = await _rockvillService.CheckCardAgainstOrder(ref_code);
            
            
            var map = new CardResultResponse();
            foreach(var d in data.data.results.ToList())
            {
                map.claim_url = d.claim_url;
                map.card_number = d.card_number;
                map.pin_code = d.pin_code;
               
            }
           await _emailService.SendInvoiceMail(
                                           to: "monjurul.gakk@gmail.com",
                                           subject: "Order Details",
                                           cardResultResponse:map,
                                           orderResponseForEmail: orderResponse

                                           
                                           );
            return Ok(map);
        }
        [HttpPost("report")]
        public async Task<IActionResult> OrderWithBkashReport()
        {

            var answer = (from p in _context.Orders
                          join v in _context.OrderDetails on p.Id equals v.order.Id into subs
                          from sub in subs.DefaultIfEmpty()
                          group sub by new { p.Id } into gr
                          select new 
                          {
                              gr.Key.Id,
                              
                              
                          });
            var ans = Task.FromResult(answer.ToListAsync());

            return Ok(answer);
            //  var data = await resultData.ToListAsync();
            //var date = new DateTime(2021 - 12 - 14);
            //var str_date = date.ToString("2021/12/14");
            //var str_date_end = date.ToString("2021/12/15");
            //var s =await  _context.Orders.Where(p => p.CreatedOn.Date== Convert.ToDateTime(str_date)).Include(d =>d.OrderDetails).ToListAsync();
            //var dateBetween = _context.Orders.Where(p => p.CreatedOn.Date >= Convert.ToDateTime(str_date)).Where(p => p.CreatedOn <= Convert.ToDateTime(str_date_end)).ToList();
            //var pro = await _context.bKashCreateAgreementRequests.FirstOrDefaultAsync(s => s.merchantInvoiceNumber == "ssss");
            //var orderWithDetails = await _context.Orders.Where(s =>s.UserId.ToString()== "9221910e-97af-4802-b189-8d66fcb4690b").
            //    Include(o => o.OrderDetails).ToListAsync();
            //var detailsList = new List<OrderReportVM>();

            //detailsList = await _context.Orders.Where(s => s.UserId.ToString() == "9221910e-97af-4802-b189-8d66fcb4690b"). 
            //    Select(d => new OrderReportVM
            //    {
            //        OrderAmount = d.OrderAmount,
            //        CreatedOn = d.CreatedOn,
            //        Orderid = d.Id,




            //        OrderDetailsReportVMs = d.OrderDetails.Select(o => new OrderDetailsReportVM
            //        {
            //            Sku = o.Sku,
            //            Price = o.Price,
            //            Title = o.Title,
            //            ImageUrlPath = o.ImageUrlPath,
            //            StoreId = o.StoreId
            //        }).ToList()

            //    }).ToListAsync();






            //foreach (var a in detailsList.ToList() )
            //{
            //    var BkashReqModel = new OrderReportVM();
            //    var BkashReq = new bKashCreateAgreementRequestReportVM();
            //    var BkashReqVM = _context.bKashCreateAgreementRequests.Where(s => s.merchantInvoiceNumber == a.Orderid.ToString())
            //    .Select(p =>
            //    new bKashCreateAgreementRequestReportVM
            //    {
            //        mode = p.mode,
            //        payerReference = p.payerReference,
            //        callbackURL = p.callbackURL,
            //        amount = p.amount,
            //        currency = p.currency,
            //        intent = p.intent,
            //        agreementID = p.agreementID,
            //        merchantInvoiceNumber = p.merchantInvoiceNumber
            //    });




            //   // detailsList.Add(BkashReqModel);





            //}


          //  return Ok(resultData);
       }

    }
}

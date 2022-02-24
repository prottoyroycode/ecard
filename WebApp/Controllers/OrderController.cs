using Library.Core.Infrastructure;
using Library.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models.DataContext;
using Models.Entities;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using ViewModels.OrderViewModels;
using ViewModels.RockVilleResponse;
using System.IO;
using Microsoft.AspNetCore.Cors;
using ViewModels.RockVIlleRequest;
using Services.Version_1.Securites;

namespace WebApi.Controllers
{


    [EnableCors]

    public class OrderController : BaseApiController
    {
        private readonly IRetrieveDataFromTokenService _tokenService;
        private static readonly HttpClient client = new HttpClient();
        private readonly ITokenFetch _tokenGetService;
        private readonly IOrderService _orderService;
        private readonly IRockvillService _rockvillService;
        private readonly IbkashTransectionService _bkashTransection;
        private readonly IUserService _userService;

        public OrderController(IOrderService orderService, 
            IRetrieveDataFromTokenService tokenService, 
            ITokenFetch tokenGetService, 
            IRockvillService rockvillService,
            IbkashTransectionService bkashTransection,
            IUserService userService



            )
        {
            _tokenService = tokenService;
            _tokenGetService = tokenGetService;
            _orderService = orderService;
            _rockvillService = rockvillService;
            _bkashTransection = bkashTransection;
            _userService = userService;
             
           
        }
       
        [HttpPost("rockVille-order-test")]
        public async Task<IActionResult> RockVilleOrdertestApi([FromBody] Order order)
        {
            var tokenObject = await _tokenGetService.GetTokenAsync();

            var mainToken = tokenObject.Message.ToString();
            var data = await _rockvillService.PlaceOrderRockVilleAsync(order, order.Email, mainToken);
            return Ok(data);
        }


            [HttpPost("order-callback")]
        public async Task<IActionResult> OrderCallBack( [FromBody] CallBackModel data)
        {
            var responseData = data;
            var cardNumbersString = new List<string>();
            var pin_codeString = new List<string>();
            //foreach(var s in string.Concat(data.cards.results.ToList())
            //{
            //    cardNumbersString.Add(s.card_number);
            //    pin_codeString.Add(s.pin_code);
            //}

            var cardResultData = Newtonsoft.Json.JsonConvert.SerializeObject(data.cards.results).ToString();

            var response = new Response();
            var resultData = new CallbackData()
            {
                success = data.success,
                responseCode = data.responseCode,
                description = data.description,
                order_id = data.order.order_id,
                email = data.order.email,
                status = data.order.status,
                status_text = data.order.status_text,
                created_time = data.order.created_time,
                reference_code = data.order.reference_code,
                count = data.order.count,
                unit_price = data.order.unit_price,
                is_completed = data.order.is_completed,
                sku = data.order.product.sku,
                title = data.order.product.title,
                next = data.cards.next,
                previous = data.cards.previous,
                CardResults = cardResultData,
                CardCount = data.cards.count
                //card_number = string.Join(", ",cardNumbersString),
                //pin_code =string.Join(" ,",pin_codeString)

            };
            var result =await _rockvillService.InserCallBackData(resultData);
            return Ok(result);


        }
        [Authorize]
        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order orderVM)
        {
            
            //var tokenObject = await _tokenGetService.GetTokenAsync();

            //var mainToken = tokenObject.Message.ToString();

            Response apiResponse = new Response {StatusCode = ResStatusCode.BadRequest , Message="empty Object returns empty" , Data = null};

            var tokenHolder = _tokenService.GetClaims();

            orderVM.CreatedBy = tokenHolder.UserId.ToString();
            orderVM.UserId = new Guid(tokenHolder.UserId) ;
            orderVM.CreatedOn = DateTime.UtcNow;
            orderVM.Email = tokenHolder.Email;


            var getPhone = await _userService.GetUserByIdAsync(orderVM.UserId);

            var phone = (getPhone.Data!=null ? (UserViewModelFOrUpdate)(getPhone.Data): new UserViewModelFOrUpdate()).Mobile;
            orderVM.OrderAmount = orderVM.OrderDetails.Sum(a => a.Price);
            var catalogCheckModel = orderVM.OrderDetails.Select(sku => new CatalogAvailabilityVM
            {
                Sku = sku.Sku,
                Item_Count = sku.Quantity,
                Order_Price = sku.Price

            }).ToList();



            #region check item exists ?
          //  var catalogCheckService = await _orderService.CHeckCategoryAvailabilityAsync(catalogCheckModel, mainToken);
           
            
            //if (catalogCheckService.Status == false)
            //    return Ok(new Response
            //    {
            //        Status = false,
            //        Message = "item is not available",
            //        StatusCode = ResStatusCode.NoDataAvailable
            //    });
            #endregion
            var order = (Order)orderVM;

            foreach (var item in order.OrderDetails)
            {
                item.OrderDetailsId = Guid.NewGuid();
            }
           
           
            var storeOrderInDb =await _orderService.CreateOrderAsync(order);
           

            if (storeOrderInDb.StatusCode == ResStatusCode.Success)
            {
                /// Payment Bkash
                /// 

                Order aData = (Order)storeOrderInDb.Data;
                var aggreementResponse = await _bkashTransection.CreateAgreement(aData.OrderAmount.ToString(), aData.Id.ToString(), phone);
                //var aggreementResponse = await _bkashTransection.CreateAgreementWithAgreementId(aData.OrderAmount.ToString(), aData.Id.ToString(), phone);

                OrderVM aOrdervm = new OrderVM(aData);
                aOrdervm.bKashUrl = aggreementResponse.bkashURL;
                if (string.IsNullOrEmpty(aggreementResponse.bkashURL))
                {
                    return Ok( new { Message = aggreementResponse.statusMessage,  StatusCode = aggreementResponse.statusCode, Status = false });
                }
                //if (false)
                //{
                //    return StatusCode(500, new Response { Message = "Success", Data = aOrdervm, StatusCode = ResStatusCode.Success, Status = true })
                //}

                return Ok(new Response { Message = "Success", Data = aOrdervm, StatusCode = ResStatusCode.Success, Status = true });
            }
            
            //if(storeOrderInDb.Status==true)
            return Ok(apiResponse);
        }
        [HttpPost("rockVille_order")]
        public async Task<IActionResult> RockVileOrder([FromBody] OrderTestRockVilleVM orderRockVilleVM)
        {
            var data = await _rockvillService.CallRockVilleOrderApiAsync(orderRockVilleVM);
            return Ok(data);

        }

        [HttpPost("rockVille_order_data_store")]
        public async Task<IActionResult> RockVileOrderDataStore([FromBody] OrderTestRockVilleVM orderRockVilleVM)
        {
            var data = await _rockvillService.StoreRcokVilleOrderDataIntoDb(orderRockVilleVM);
            return Ok(data);

        }
        [HttpGet("checkOrderByRefCode")]
        public async Task<IActionResult> CheckOrderByReference(string reference_code)
        {
            var tokenObject = await _tokenGetService.GetTokenAsync();

            var token = tokenObject.Message.ToString();
            var data = await _orderService.GetOrderByReferenceCodeAsync(reference_code,token);
            return Ok(data);
        }


    }
}

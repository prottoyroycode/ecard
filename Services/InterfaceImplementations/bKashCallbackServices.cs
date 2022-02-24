using Models.bKash;
using Models.DataContext;
using Services.Interfaces;
using Services.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Library.Core.ViewModels;
using Library.Core.ViewModels.historyViewModels;
using ViewModels.RockVIlleRequest;
using Library.Core.Enums;

namespace Services.InterfaceImplementations
{
    public class bKashCallbackServices : GenericRepository<bKashCallback> ,IbKashCallbackServices
    {
        private readonly EfDbContext _context;
        private readonly IOrderService _orderService;
        private readonly IRockvillService _rockvillService;
        private string paymentID;
        private readonly ITokenFetch _tokenGetService;

        public bKashCallbackServices(EfDbContext context , IOrderService orderService, IRockvillService rockvillService, ITokenFetch tokenGetService) :  base(context)
        {
            _context = context;
            _orderService = orderService;
            _rockvillService = rockvillService;
            _tokenGetService = tokenGetService;
        }
        public async Task<Response> SaveCallbackAndUpdateOrderAsync(bKashCallback callback)
        {
            var tokenObject = await _tokenGetService.GetTokenAsync();

            var token = tokenObject.Message.ToString();
            
            await  _context.bKashCallbacks.AddAsync(callback);
            Response aResponse;
            _context.SaveChanges();
            if (callback.status == "success")
            {
                var bkashResponseByPaymentId = await _context.bKashCreateAgreementResponses.FirstOrDefaultAsync(a => a.paymentID == callback.paymentID);
                var order = await _context.Orders.Include( a => a.OrderDetails).FirstOrDefaultAsync(a => a.Id == Guid.Parse(bkashResponseByPaymentId.merchantInvoiceNumber));
                var callRockVileOrder = await _rockvillService.PlaceOrderRockVilleAsync(order, order.Email, token);



                return new Response { Data = order, Status = true, StatusCode = ResStatusCode.Success, Message = "Order Created" };

              
            }
            else return new Response { Data = null, Status = false, StatusCode = ResStatusCode.InternalServerError, Message = "Smoething wrong in order creation callback" };


        }
    }
}

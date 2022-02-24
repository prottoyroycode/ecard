using Library.Core.ViewModels;
using Models.Entities;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.OrderViewModels;

namespace Services.Interfaces
{
    public interface IOrderService :IGenericRepository<Order>
    {

        Task<Response> CreateOrderAsync(Order order);
        Task<Response> CHeckCategoryAvailabilityAsync(List<CatalogAvailabilityVM> catalogAvailabilityVM,string token);
        Task<Response> GetOrderByReferenceCodeAsync(string reference_code,string token);
       // Task<Response> PlaceOrderToRockVilleAsync(Order order);
        

    }
}

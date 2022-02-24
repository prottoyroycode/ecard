using Library.Core.ViewModels;
using Models.DataContext;
using Models.Entities;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Repository.Implementation;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModels.OrderViewModels;
using ViewModels.RockVilleResponse;

namespace Services.InterfaceImplementations
{
    public class OrderService :GenericRepository<Order>, IOrderService 
    {

        
        private readonly EfDbContext _context;
        
        public OrderService(
            EfDbContext context
            
            ) : base(context)
        {
            _context = context;
            
        }

        public async Task<Response> CHeckCategoryAvailabilityAsync(List<CatalogAvailabilityVM> catalogAvailabilityVM,string token)
        {
            var response = new Response();
            try
            {
                foreach(var s in catalogAvailabilityVM.ToList())
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                       // string token = tokenObject.Message.ToString();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var url = string.Format("https://ezvoucher.rockvillegroup.com/ezvoucher/orders/{0}/availability?item_count={1}&price={2}", s.Sku, s.Item_Count, s.Order_Price);
                        // New code:
                        var httpResponse = await client.GetAsync(url);

                        if (httpResponse.IsSuccessStatusCode)
                        {
                            var result = httpResponse.Content.ReadAsStringAsync().Result;

                           var resultObject = JsonConvert.DeserializeObject<CheckCategory>(result);
                            if (resultObject.data.availability == false)
                            {
                                response.Status = false;
                                return response;
                                //result = httpResponse.Content.ReadAsStringAsync().Result;

                                //resultObject = JsonConvert.DeserializeObject<CheckCategory>(result);
                                //if (resultObject.data.availability == true)
                                //{
                                //    response.Status = true;
                                //    return response;
                                //}
                                //else
                                //{
                                // response.Data = resultObject;

                                // }


                                //
                            }
                            else
                            {
                             //   response.Data = resultObject;
                                response.Status = true;
                                return response;

                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                
            }
            return response;
           
        }

        public async Task<Response> CreateOrderAsync(Order order)
        {
            
            try
            {
                
                await base.InsertAsync(order);
                await _context.SaveChangesAsync();
                return new Response()
                {
                    Status = true,
                    StatusCode = ResStatusCode.Success,
                    Data = order,
                    Message = "order submitted",
                    TotalRecords = order.OrderDetails.Count()

            };
            }
            catch (Exception ex)
           {


                await _context.RollbackTranAsync();
                //var messages = new List<string>();
                //do
                //{
                //    messages.Add(ex.Message);
                //    ex = ex.InnerException;
                //}

                //while (ex != null);

                return new Response() { Status = false, Message = ex.Message,Data=null };

            }
        }

        public async Task<Response> GetOrderByReferenceCodeAsync(string reference_code,string token)
        {
            var response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // string token = tokenObject.Message.ToString();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var url = string.Format("https://ezvoucher.rockvillegroup.com/ezvoucher/orders/{0}",reference_code);
                    // New code:
                    var httpResponse = await client.GetAsync(url);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var result = httpResponse.Content.ReadAsStringAsync().Result;
                        var resultObject = JsonConvert.DeserializeObject<GetOrderByRef>(result);
                        if(resultObject.success==true)
                        {
                            response.Data = resultObject.data;
                            response.Status = true;
                            response.Message = "orders";
                        }
                        else
                        {
                            response.Data = resultObject.data;
                            response.Status = false;
                            response.Message = resultObject.description;
                            response.StatusCode = (ResStatusCode)resultObject.responseCode;
                        }
                       

                        //  var resultObject = JsonConvert.DeserializeObject<CheckCategory>(result);


                    }
                    if(!httpResponse.IsSuccessStatusCode)
                    {
                        response.Status = false;
                        response.Data = null;
                        response.StatusCode = ResStatusCode.BadRequest;
                        response.Message = "some thing wen wrong";
                    }
                }
            }
            catch(Exception ex)
            {
               response.Status = false;
                response.Data = null;
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

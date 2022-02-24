using Library.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models.DataContext;
using Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Catalog;
using ViewModels.RockVIlleRequest;
using ViewModels.RockVilleResponse;

namespace Services.InterfaceImplementations
{
    public class RockvillService : IRockvillService
    {
        private readonly ITokenFetch _tokenService;
        private readonly EfDbContext _context;
        private readonly IFavouriteService _favoriteService;
        private readonly IEmailService _emailService;


        public RockvillService(ITokenFetch tokenService , EfDbContext context , IFavouriteService favoriteService, IEmailService emailService) 
        {
            _tokenService = tokenService;
            _context = context;
            _favoriteService = favoriteService;
            _emailService = emailService;
        }

        public async Task<Response> GetNetFlixCatalogue(string filter)
        {
            try
            {

                var result = new Response();


                if (string.IsNullOrEmpty(filter))
                {
                    result.Data = null;
                    result.Message = "No Keyword found";
                    result.StatusCode = ResStatusCode.BadRequest;
                    return result;
                }


                #region readJson

                //string jsonString = "";
                //JObject o2;
                //var filePathWithBase = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\" + "json", "RockVillResponse.json");
                //using (StreamReader r = new StreamReader(filePathWithBase))
                //{
                //    jsonString = r.ReadToEnd();


                //}

                //var jj = JObject.Parse(jsonString).ToString();
                //var  data =  JsonConvert.DeserializeObject<CatalogueResponse>(jj);



                #endregion


                #region API CALL
                //var token = await _tokenService.GetTokenAsync();


                //var client = new RestClient("https://ezvoucher.rockvillegroup.com/ezvoucher/catalogs?limit=10000&offset=0");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("Authorization", "Bearer "+token.Message.ToString());

                //IRestResponse<Response> restResponse = client.Get<Response>(request);

                //JsonDeserializer deserealizer = new JsonDeserializer();
                //CatalogueResponse data = deserealizer.Deserialize<CatalogueResponse>(restResponse);
                #endregion

                var data = await _context.ECardVouchers.Where(a => a.card_type == filter.ToLower() && a.IsActive==true).ToListAsync();
                if (data.Count>0)
                {
                    //var aList = data.data.results.Where(a => a.title.ToLower().Trim().Contains(!string.IsNullOrEmpty(filter) ? filter.ToLower().Trim() : filter)).ToList();

                    //foreach (var item in aList)
                    //{
                    //    if (item.min_price == 100)
                    //    {
                    //        item.title = "Netflix BD, 103 Days";
                    //        item.min_price = 2400;
                    //        item.min_price_str = "৳ 2,400";
                    //        item.image = "https://api.evouchers.store/netflix/netFlix1.png";
                    //    }
                    //    else if (item.min_price == 500)
                    //    {
                    //        item.min_price = 12000;
                    //        item.title = "Netflix BD, 517 Days";
                    //        item.min_price_str = "৳ 12,000";
                    //        item.image = "https://api.evouchers.store/netflix/netFlix2.png";
                    //    }
                    //}
                    if (data.Count == 0)
                    {
                        result.Data = data;
                        result.Message = "Records not found";
                        result.StatusCode = ResStatusCode.NoDataAvailable;
                    }
                    else
                    {
                        result.Data = data;
                        result.Message = "Records found";
                        result.StatusCode = ResStatusCode.Success;
                    }
                    result.Status = true;
                    
                    result.TotalRecords = data.Count;
                    return result;
                }
                else
                {
                    result.Data = null;
                    result.Message = "No Records found";
                    result.Status = true;
                    result.StatusCode = ResStatusCode.NoDataAvailable;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var result = new Response();
                result.Data = null;
                result.Message = ex.ToString();
                result.Status = false;
                result.StatusCode = ResStatusCode.InternalServerError;
                return result;

                throw;
            }

           
        }

        public async Task<Response> InserCallBackData(CallbackData data)
        {
            var response = new Response();
           
            try
            {
                await _context.CallbackDatas.AddAsync(data);
                var isInserted = await _context.SaveChangesAsync();
                
                response.Status = true;
                response.Message = "data inserted successfully";
                response.StatusCode = ResStatusCode.Success;
                response.Data = data;
                //return response;
                
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = "error inserting data";
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Data = new { };
            }
            return response;
        }


        public async Task<Response> GetNetFlixCatalogue(string filter , Guid user)
        {
            try
            {

                var result = new Response();


                if (string.IsNullOrEmpty(filter))
                {
                    result.Data = null;
                    result.Message = "No Keyword found";
                    result.StatusCode = ResStatusCode.BadRequest;
                    return result;
                }


                #region readJson

                //string jsonString = "";
                //JObject o2;
                //var filePathWithBase = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\" + "json", "shared.json");
                //using (StreamReader r = new StreamReader(filePathWithBase))
                //{
                //    jsonString = r.ReadToEnd();


                //}

                //var jj = JObject.Parse(jsonString).ToString();
                //var data = JsonConvert.DeserializeObject<CatalogueResponse>(jj);
                //var dataFav = _context.Favourites.Where(a => a.UserId == user).Select(a => a.Sku).ToList();
                //data.data.favorites = dataFav;
                #endregion


                #region API CALL
                //var token = await _tokenService.GetTokenAsync();


                //var client = new RestClient("https://ezvoucher.rockvillegroup.com/ezvoucher/catalogs?limit=10000&offset=0");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("Authorization", "Bearer "+token.Message.ToString());

                //IRestResponse<Response> restResponse = client.Get<Response>(request);

                //JsonDeserializer deserealizer = new JsonDeserializer();
                //CatalogueResponse data = deserealizer.Deserialize<CatalogueResponse>(restResponse);
                #endregion
                var data = await _context.ECardVouchers.Where(a => a.card_type == filter.ToLower() && a.IsActive==true).ToListAsync();
                var dataFav = _context.Favourites.Where(a => a.UserId == user).Select(a => a.Sku).ToList();
                if (data.Count > 0)
                {
                    var aList = data.ToList();
                    Tuple<List<ECardVoucher>, List<string>> withFavorite = new Tuple<List<ECardVoucher>, List<string>>(aList, dataFav);
                     
                    if (withFavorite.Item1.Count == 0)
                    {
                        result.Data = withFavorite;
                        result.Message = "Records not found";
                        result.StatusCode = ResStatusCode.NoDataAvailable;
                    }
                    else
                    {
                        result.Data = withFavorite;
                        result.Message = "Records found";
                        result.StatusCode = ResStatusCode.Success;
                    }
                    result.Status = true;

                    result.TotalRecords = aList.Count;
                    return result;
                }
                else
                {
                    result.Data = null;
                    result.Message = "No Records found";
                    result.Status = true;
                    result.StatusCode = ResStatusCode.NoDataAvailable;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var result = new Response();
                result.Data = null;
                result.Message = ex.ToString();
                result.Status = false;
                result.StatusCode = ResStatusCode.InternalServerError;
                return result;

                throw;
            }


        }

        public async Task<Response> PlaceOrderRockVilleAsync(Order order, string email, string token)
        {
            List<string> cardInfos = new List<string>();
           
            var response = new Response();
            
            var orderMapping =  order.OrderDetails.Select( s => new OrderTestRockVilleVM
            {
                sku = s.Sku,
                quantity = 1,
                price =Convert.ToDecimal(_context.ECardVouchers.Where(a => a.sku == s.Sku && a.IsActive == true).FirstOrDefault().main_min_price),
                destination = "",
                delivery_type = 0,
                pre_order = 0,
                reference_code = s.OrderDetailsId,
                terminal_id = 1016,
                terminal_pin = 1918


            }); ;
            foreach (var d in orderMapping.ToList())
            {
                var saveOrderDataIntoDb = await StoreRcokVilleOrderDataIntoDb(d);
                try
                {

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                        //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        //string token = tokenObject.Message.ToString();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var url = " https://ezvoucher.rockvillegroup.com/ezvoucher/orders";
                        var json = JsonConvert.SerializeObject(d);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");
                        // New code:
                        var httpResponse = await client.PostAsync(url, data);
                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            string result = httpResponse.Content.ReadAsStringAsync().Result;
                        }
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            // //store response into db
                            string result = httpResponse.Content.ReadAsStringAsync().Result;
                            var s = result.ToString();


                            var jsonDeserialize = JsonConvert.DeserializeObject<OrderResponse>(result);


                            string dataStoreData = JsonConvert.SerializeObject(jsonDeserialize.data);
                            //var isSuccessfulRequest = jsonDeserialize.description == "Success";
                            jsonDeserialize.reference_code = d.reference_code;
                            jsonDeserialize.dataStore = dataStoreData;
                            var storeResponse = _context.OrderResponses.AddAsync(jsonDeserialize);
                            await _context.SaveChangesAsync();
                            if (jsonDeserialize.description == "Success")
                            {
                                //var detailsData = await _context.OrderDetails.Include(odr =>odr.order).FirstOrDefaultAsync(d => d.OrderDetailsId == 
                                //jsonDeserialize.reference_code);

                                var detailsData = await _context.User.FirstOrDefaultAsync(a => a.Id == order.UserId);
                                var map = new CardResultResponse();
                                if (detailsData !=null)
                                {
                                    var getCardsByrefcode = await CheckCardAgainstOrder(d.reference_code.ToString());
                                    var orderDetails = await _context.OrderDetails.FirstOrDefaultAsync(order =>order.OrderDetailsId==d.reference_code);
                                    var orderResponse = new OrderResponseForEmail();
                                    orderResponse.productName = orderDetails.Title;
                                    orderResponse.price = orderDetails.Price;
                                    

                                    if (getCardsByrefcode.data.results.Count>0)
                                    {
                                        foreach(var c in getCardsByrefcode.data.results.ToList())
                                        {

                                            map.claim_url = c.claim_url;
                                            map.card_number = c.card_number;
                                            map.pin_code = c.pin_code;
                                        }
                                        await _emailService.SendInvoiceMail(
                                            to: detailsData.Email,
                                            subject: "Order Details",
                                            cardResultResponse: map,
                                            orderResponseForEmail: orderResponse
                                            );
                                     
                                        
                                    }
                                    
                                }
                            }


                            



                            response.Data = d;
                            response.Message = "order placed successfully";
                            response.Status = true;
                            response.TotalRecords = order.OrderDetails.Count();
                        }

                        else
                        {
                            response.Data = null;
                            response.Message = "server error";
                            response.Status = false;
                            response.StatusCode = ResStatusCode.InternalServerError;

                        }



                    }

                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.StatusCode = ResStatusCode.BadRequest;
                    response.Message = ex.Message;
                    response.Data = null;
                }
            }
           

            return response;

        }

        public async Task<Response> CallRockVilleOrderApiAsync(OrderTestRockVilleVM orderVM)
        {
            var orderResponse = new OrderRockVilleVM();
            var tokenObject = await _tokenService.GetTokenAsync();
            var token = tokenObject.Message.ToString();
            var response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    var requestDataJson = JsonConvert.SerializeObject(orderVM);
                    StringContent data = new StringContent(requestDataJson, Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                   // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    
                    var url = "https://ezvoucher.rockvillegroup.com/ezvoucher/testOrders";
                    var httpResponse = await client.PostAsync(url, data);
                    
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        
                        string result = await httpResponse.Content.ReadAsStringAsync();
                        
                        orderResponse = JsonConvert.DeserializeObject<OrderRockVilleVM>(result);
                        response.Status = true;
                        response.StatusCode = ResStatusCode.Success;
                        response.Message = "order placed suscessfully";
                        response.Data = result.ToString();
                        client.Dispose();

                    }
                    if(!httpResponse.IsSuccessStatusCode)
                    {
                        response.Message = "failed";
                        response.Status = false;
                        response.StatusCode = ResStatusCode.BadRequest;
                    }
                    


                }
                
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response> StoreRcokVilleOrderDataIntoDb(OrderTestRockVilleVM orderRockVilleVM)
        {
            var mapData = new RockVilleOrderData
            {
                sku = orderRockVilleVM.sku,
                quantity = orderRockVilleVM.quantity,
                price = orderRockVilleVM.price,

               // email = orderRockVilleVM.email,
                pre_order = orderRockVilleVM.pre_order,
                reference_code = orderRockVilleVM.reference_code,
             //   store_id = orderRockVilleVM.store_id
                // email = orderRockVilleVM.email,
               
               
                //   store_id = orderRockVilleVM.store_id

            };
            var response = new Response();
            try
            {
                var insertData = await _context.RockVilleOrderDatas.AddAsync(mapData);
                var isInserted = await _context.SaveChangesAsync();
                if (isInserted > 0)
                {
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Success;
                    response.Message = "inserted successfully";
                    response.Data = orderRockVilleVM;
                }
                else
                {
                    response.Status = false;
                    response.StatusCode = ResStatusCode.BadRequest;
                    response.Message = "insertion failed";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = ResStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<RockVilleCardResponse> CheckCardAgainstOrder(string ref_code)
        {
            var res = new RockVilleCardResponse();
            
            var tokenObject = await _tokenService.GetTokenAsync();

            var mainToken = tokenObject.Message.ToString();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // string token = tokenObject.Message.ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mainToken);
                //var url = string.Format("https://ezvoucher.rockvillegroup.com/ezvoucher/orders/{0}/availability?item_count={1}&price={2}", 
                //    s.Sku, s.Item_Count, s.Order_Price);
                var url = string.Format("https://ezvoucher.rockvillegroup.com/ezvoucher/orders/{0}/cards?limit={1}&offset={2}",
                   ref_code,10,0);
                
                // New code:
                var httpResponse = await client.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Content.ReadAsStringAsync().Result;
                    var desrializedObject =JsonConvert.DeserializeObject<RockVilleCardResponse>(result);
                    var mainData = desrializedObject.data.results;
                    res = desrializedObject;
                    return res;
                   // res.dataStore = mainData.ToString();
                   // return res;
                    // var resultObject = JsonConvert.DeserializeObject<CheckCategory>(result);



                }
            }
            return res;
        }
    }
}

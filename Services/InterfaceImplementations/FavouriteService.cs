using Library.Core.ViewModels;
using Library.Core.ViewModels.historyViewModels;
using Microsoft.EntityFrameworkCore;
using Models.DataContext;
using Models.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.favouriteViewModels;


namespace Services.InterfaceImplementations
{
    
    public class FavouriteService : IFavouriteService
    {
        public readonly EfDbContext _context;
        public FavouriteService(EfDbContext context)
        {
            _context = context;
        }
        public async Task<Response> AddToFavouriteAsync(Favourite favourite)
        {

            var favMapping = new
            {
                title = favourite.Title,
                price = favourite.Price,
                sku = favourite.Sku
            };

            var serviceResponse = new Response();
            try
            {
                var userData = await _context.Favourites.Where(u =>u.UserId==favourite.UserId).ToListAsync();
                if(userData.Any(s=>s.Sku == favourite.Sku))
                {
                    serviceResponse.Status = false;
                    serviceResponse.Message = "already in your favourite list";
                    serviceResponse.StatusCode = ResStatusCode.BadRequest;
                    //serviceResponse.Data = favMapping;
                }
               else
                {
                    var data = await _context.AddAsync(favourite);
                    var isInserted = await _context.SaveChangesAsync();
                    if (isInserted == 0 || isInserted != 1)
                    {
                        serviceResponse.Status = false;
                        serviceResponse.Message = "problem adding into favourite list";
                        serviceResponse.StatusCode = ResStatusCode.InternalServerError;
                        serviceResponse.Data = null;
                    }

                    serviceResponse.Status = true;
                    serviceResponse.Message = "added to your favourite list";
                    serviceResponse.StatusCode = ResStatusCode.Success;
                    serviceResponse.Data = favMapping;
                }
                
                return serviceResponse;




            }
            catch (Exception ex)
            {
                serviceResponse.Status = false;
                serviceResponse.Data = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<Response> GetMyFavouriteListAsync(Guid userId)
        {
            var response = new Response();
            try
            {
                var favListdata = await _context.Favourites.Where(u => u.UserId == userId).ToListAsync();
                response.Status = true;
                response.StatusCode = ResStatusCode.Success;
                response.Message = "your favourite list";
                response.Data = favListdata;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response> GetUserOrderHistory(Guid userId)
        {
            var response = new Response();
            try
            {
                
                var hisData =await _context.Orders.Where(u => u.UserId == userId).Select(o => new OrderVM
                {
                    CreatedOn = o.CreatedOn,
                    OrderAmount =o.OrderAmount,
                    OrderDetails = o.OrderDetails.Select(d => new OrderDetailsVM
                    {
                        Sku = d.Sku,
                        StoreId = d.StoreId,
                        ImageUrlPath = d.ImageUrlPath,
                        Title = d.Title,
                        Price = d.Price
                    }).ToList()
                }).ToListAsync();
                if(hisData.Count>0)
                {
                    response.Status = true;
                    response.StatusCode = ResStatusCode.Success;
                    response.Message = "your order history";
                    response.TotalRecords = hisData.Count();
                    response.Data = hisData;
                }
                if(hisData.Count==0)
                {
                    response.Status = true;
                    response.StatusCode = ResStatusCode.NoDataAvailable;
                    response.Message = "no data found in your order history";
                    response.TotalRecords = hisData.Count();
                    response.Data = null;
                }
                
               
                
            }
            catch(Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                response.StatusCode = ResStatusCode.InternalServerError;
            }
            return response;
     
        }

        public async Task<Response> RemoveFromFavouriteAsync(Guid userId,string sku)
        {
            var response = new Response();
            try
            {
                var dataTobeDeleted = await _context.Favourites.FirstOrDefaultAsync(s => s.Sku == sku && s.UserId == userId);
                var result = _context.Remove(dataTobeDeleted);
                _context.SaveChanges();
                response.Status = true;
                response.StatusCode = ResStatusCode.Success;
                response.Message = "removed from favourite list";
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Data = ex.Message;
                response.Message = "inner exception is occured";
            }
            return response;

        }
    }

}

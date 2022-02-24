using Library.Core.ViewModels;

using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.favouriteViewModels;

namespace Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<Response> AddToFavouriteAsync(Favourite favourite);
        Task<Response> GetMyFavouriteListAsync(Guid userId);
        Task<Response> RemoveFromFavouriteAsync(Guid userId, string sku);
        Task<Response> GetUserOrderHistory(Guid userId);

    }

}

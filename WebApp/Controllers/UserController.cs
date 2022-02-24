using Library.Core.Infrastructure;
using Library.Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataContext;
using Models.Entities;
using Services.Interfaces;
using Services.Version_1.Securites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Securities;
using ViewModels.favouriteViewModels;
using ViewModels.UserViewModels;

namespace WebApi.Controllers
{
    
    public class UserController:BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IRetrieveDataFromTokenService _tokenService;
        private readonly IFavouriteService _FavouriteService;

        public UserController(IUserService userService, IRetrieveDataFromTokenService tokenService, IFavouriteService FavouriteService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _FavouriteService = FavouriteService;
        }
        [HttpGet()]
        public async Task<ActionResult> GetUserByIdAsync()
        {
            var tokenHolder = _tokenService.GetClaims();
            var userData = await _userService.GetUserByIdAsync(Guid.Parse(tokenHolder.UserId));
            return Ok(userData);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserProfile( UserProfileViewModel model)
        {
            var data =await _userService.UpdateUserProfile(model);
            return Ok(data);
        }
        [Authorize]
        [HttpGet("history")]
        public async Task<IActionResult>UserHistory()
        {
            var tokenHolder = _tokenService.GetClaims();
            var userId = Guid.Parse(tokenHolder.UserId.ToString());
            var result = await _FavouriteService.GetUserOrderHistory(userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("add-to-favourite")]
        public async Task<IActionResult> AddToFavourite([FromBody] Favourite favouriteVm)
        {
            var tokenHolder = _tokenService.GetClaims();
            var favourite = new Favourite();
            favourite.UserId = Guid.Parse(tokenHolder.UserId);
            favourite.Title = favouriteVm.Title;
            favourite.Sku = favouriteVm.Sku;
            favourite.ImageUrlPath = favouriteVm.ImageUrlPath;
            favourite.Price = favouriteVm.Price;

            var result = await _FavouriteService.AddToFavouriteAsync(favourite);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("favourite-list")]
        public async Task<IActionResult> FavouriteListbyUser()
        {
            var tokenHolder = _tokenService.GetClaims();
            var userId = Guid.Parse(tokenHolder.UserId);
            var result = await _FavouriteService.GetMyFavouriteListAsync(userId);
            return Ok(result);
        }
        [HttpDelete("remove-favourite-list/{sku}")]
        public async Task<IActionResult> RemoveFromFavouriteList(string sku)
        {
            var tokenHolder = _tokenService.GetClaims();
            var userId = Guid.Parse(tokenHolder.UserId);
            var data = await _FavouriteService.RemoveFromFavouriteAsync( userId,sku);
            return Ok(data);
        }

    }
}

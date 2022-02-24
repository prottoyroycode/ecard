using Library.Core.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetflixController : ControllerBase
    {
        private readonly ITokenFetch _tokenGetService;
        private readonly IRockvillService _rockVillService;
        private readonly IRetrieveDataFromTokenService _tokenService;

        public NetflixController(ITokenFetch tokenGetService , IRockvillService rockvillService , IRetrieveDataFromTokenService tokenService)
        {
            _tokenGetService = tokenGetService;
            _rockVillService = rockvillService;
            _tokenService = tokenService;

        }


        [HttpGet]
        
        public async Task<IActionResult> Get()
        {
            var data = await _rockVillService.GetNetFlixCatalogue("netflix");
            return Ok(data);
        }

        [HttpGet]
        [Authorize]
        [Route("netflixfav")]
        public async Task<IActionResult> GetWithFavorite()


        {


            var tokenHolder = _tokenService.GetClaims();
            ;

            var data = await _rockVillService.GetNetFlixCatalogue("netflix" , Guid.Parse(tokenHolder.UserId));
            return Ok(data);
        }
    }
}

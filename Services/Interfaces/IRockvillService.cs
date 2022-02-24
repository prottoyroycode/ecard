using Library.Core.ViewModels;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.RockVIlleRequest;
using ViewModels.RockVilleResponse;

namespace Services.Interfaces
{
    public interface IRockvillService
    {

        Task<Response> GetNetFlixCatalogue(string filter);
        Task<Response> GetNetFlixCatalogue(string filter , Guid user);
        Task<Response> InserCallBackData(CallbackData data);
        Task<Response> PlaceOrderRockVilleAsync(Order order ,string email,string token);
        Task<Response> CallRockVilleOrderApiAsync(OrderTestRockVilleVM orderRockVilleVM);
        Task<Response> StoreRcokVilleOrderDataIntoDb(OrderTestRockVilleVM orderRockVilleVM);
        Task<RockVilleCardResponse> CheckCardAgainstOrder(string ref_code);



    }
}

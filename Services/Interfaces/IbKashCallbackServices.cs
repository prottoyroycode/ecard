using Library.Core.ViewModels;
using Library.Core.ViewModels.historyViewModels;
using Models.bKash;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.RockVIlleRequest;

namespace Services.Interfaces
{
    public interface IbKashCallbackServices
    {
         Task<Response> SaveCallbackAndUpdateOrderAsync(bKashCallback callback);

    }
}

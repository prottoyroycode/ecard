using Library.Core.Response;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICallbackUrlService
    {
        Task<CommonResponse> CreateCallBackUrl (CallBackUrl callbackUrl);
    }
}

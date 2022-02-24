using Library.Core.Properties;
using Library.Core.Response;
using Models.DataContext;
using Models.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceImplementations
{
    public class CallBackUrlService : ICallbackUrlService
    {
        private readonly EfDbContext _context;
        public CallBackUrlService(EfDbContext context)
        {
            _context = context;
        }
        public async Task<CommonResponse> CreateCallBackUrl(CallBackUrl callbackUrl)
        {

            var response = new CommonResponse();
            try
            {
                // var data = await _context.AddAsync(callbackUrl);
                var isInsertedIntoDb = await _context.SaveChangesAsync();
                var isInserted = 1;

                response.Status = isInserted != 0;
                response.StatusCode = isInserted != 0 ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
                response.Message = isInserted != 0 ? Resources.DataInsertOrUpdateSuccessfully : Resources.DataInsertOrUpdateFailed;

                if (isInserted != 0)
                    response.Data = callbackUrl;
                else
                    response.Data = new { };
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Data = new { };
            }
            return response;
        }
    }
}

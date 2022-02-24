using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ViewModels
{
    public class Response
    {
        public Response()
        {
            Status = true;
            StatusCode = ResStatusCode.Success;
            Message = ResStatusCode.Success.ToString();
        }

        public static object Cookies { get; set; }
        public bool Status { get; set; }
        public ResStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public int TotalRecords { get; set; }
        public object Data { get; set; }
    }

    public enum ResStatusCode
    {
        Success = 200,
        Created = 201,
        NoDataAvailable=204,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
        ExistBySocial = 1000
    }
}

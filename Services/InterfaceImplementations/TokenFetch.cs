using Library.Core.ViewModels;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewModels.UserViewModels;

namespace Services.InterfaceImplementations
{
    public class TokenFetch : ITokenFetch
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<Response> GetTokenAsync()
        {
            Response response = new Response();
            var url = "https://ezvoucher.rockvillegroup.com/auth/oauth/token";

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            });

            client.DefaultRequestHeaders.Accept.Clear();
            var byteArray = Encoding.ASCII.GetBytes("GakkMedia:4a93218e425e4264b86698609a6312f2");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var httpResponse = await client.PostAsync(url, formContent);

            dynamic result = JsonConvert.DeserializeObject(await httpResponse.Content.ReadAsStringAsync());

            if (httpResponse.IsSuccessStatusCode)
            {
                response.Status = httpResponse.IsSuccessStatusCode;
                response.StatusCode = (ResStatusCode)httpResponse.StatusCode;
                response.Message = $"{result.access_token}";
                response.Data = result;
            }
            else
            {
                response.Status = httpResponse.IsSuccessStatusCode;
                response.StatusCode = (ResStatusCode)httpResponse.StatusCode;
                response.Message = result.errorCode;
                response.Data = result;
            }

            return response;
        }
    }
}

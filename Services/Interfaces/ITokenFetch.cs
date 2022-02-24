using Library.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITokenFetch
    {
        Task<Response> GetTokenAsync();

    }
}

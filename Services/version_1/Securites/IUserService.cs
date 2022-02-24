using Library.Core.Response;
using Models.Securities;
using Services.Repository.Interface;
using System;
using System.Threading.Tasks;
using ViewModel.Securities;
using ViewModels.UserViewModels;

namespace Services.Version_1.Securites
{
    public interface IUserService : IGenericRepository<User>
    {
        Task<Tuple<bool, string, UserViewModel>> V1_UserRegistrationAsync(RegistrationViewModel model);
       
        Task<Tuple<int, string, UserViewModel>> V1_UserLoginAsync(LoginViewModel model);
        Task<Tuple<int, string, UserViewModel>> V2_UserLoginAsync(MsisdnLoginViewModel model);

        Task<object> GetUserProfile(Guid userId);

        Task<object> UpdateUserProfile(UserProfileViewModel model);
        Task<CommonResponse> GetUserByIdAsync(Guid id);
        


    }
}

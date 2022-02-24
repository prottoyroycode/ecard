using Library.Core.Common;
using Library.Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DataContext;
using Models.Securities;
using Services.Repository.Implementation;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViewModel.Securities;
using ViewModels.UserViewModels;

namespace Services.Version_1.Securites
{
    public class UserService : GenericRepository<User>, IUserService
    {
        #region Constructor

        private readonly EfDbContext _context;
        private static IConfiguration _iConfiguration;
        public UserService(
            EfDbContext context
            , IConfiguration iConfiguration
            ) : base(context)
        {
            _context = context;
            _iConfiguration = iConfiguration;
        }

        #endregion Constructor

        public async Task<Tuple<bool, string, UserViewModel>> V1_UserRegistrationAsync(RegistrationViewModel model)
        {
            UserViewModel user = new UserViewModel();
            try
            {
                DataSet ds = await _context.LoadStoredProc("[dbo].[V1_UserRegistration]")
                              .WithSqlParam("userName", model.UserName)
                              .WithSqlParam("password", model.Password)
                              .WithSqlParam("registerWith", model.RegisterWith)
                              .WithSqlParam("email", model.Email)
                              .WithSqlParam("mobile", model.Mobile)
                              .WithSqlParam("fullName", model.FullName)
                              .WithSqlParam("gender", model.Gender)
                              .WithSqlParam("fcmDeviceId", model.FcmDeviceId)
                              .WithSqlParam("country", model.Country)
                              .WithSqlParam("countryCode", model.CountryCode)
                              .WithSqlParam("city", model.City)
                              .GetDbToDataSet();

                if (ds != null && Convert.ToBoolean(ds.Tables[0].Rows[0][0]))
                    user = DataUtil.ConvertDataRowToEntity<UserViewModel>(ds.Tables[1].Rows[0]);
                return new Tuple<bool, string, UserViewModel>(Convert.ToBoolean(ds.Tables[0].Rows[0][0]), ds.Tables[0].Rows[0][1].ToString(), user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Tuple<int, string, UserViewModel>> V1_UserLoginAsync(LoginViewModel model)
        {
            UserViewModel user = new UserViewModel();
            DataSet ds = await _context.LoadStoredProc("V1_UserLogin")
                              .WithSqlParam("userName", model.UserName)
                              .WithSqlParam("password", model.Password)
                              .WithSqlParam("registerWith", model.RegisterWith)
                              .WithSqlParam("email", model.Email)
                              .WithSqlParam("mobile", model.Mobile)
                              .WithSqlParam("fullName", model.FullName)
                              .WithSqlParam("gender", model.Gender)
                              .WithSqlParam("imageUrl", model.ImageUrl)
                              .WithSqlParam("fcmDeviceId", model.FcmDeviceId)
                              .WithSqlParam("country", model.Country)
                              .WithSqlParam("countryCode", model.CountryCode)
                              .WithSqlParam("city", model.City)
                              .GetDbToDataSet();

            if (ds != null && Convert.ToBoolean(ds.Tables[0].Rows[0][0]))
                user = DataUtil.ConvertDataRowToEntity<UserViewModel>(ds.Tables[1].Rows[0]);
            return new Tuple<int, string, UserViewModel>(Convert.ToInt32(ds.Tables[0].Rows[0][0]), ds.Tables[0].Rows[0][1].ToString(), user);
        }


        public async Task<Tuple<int, string, UserViewModel>> V2_UserLoginAsync(MsisdnLoginViewModel model)
        {
            UserViewModel user = new UserViewModel();
            DataSet ds = await _context.LoadStoredProc("V2_UserLogin")
                              
                              .WithSqlParam("password", model.Password)
                             
                             
                              .WithSqlParam("mobile", model.Mobile)
                              

                              .GetDbToDataSet();

            if (ds != null && Convert.ToBoolean(ds.Tables[0].Rows[0][0]))
                user = DataUtil.ConvertDataRowToEntity<UserViewModel>(ds.Tables[1].Rows[0]);
            return new Tuple<int, string, UserViewModel>(Convert.ToInt32(ds.Tables[0].Rows[0][0]), ds.Tables[0].Rows[0][1].ToString(), user);
        }
        public async Task<object> GetUserProfile(Guid userId)
        {
            try
            {
                var userDb = await _context.User.Where(t => t.Id == userId && t.IsActive).FirstOrDefaultAsync();

                if (userDb == null) throw new Exception("User not available");

                object obj = new
                {
                    userDb.FullName,
                    userDb.Email,
                    BirthDate = userDb.BirthDate.HasValue ? Convert.ToDateTime(userDb.BirthDate).ToString("dd MMM yyyy") : "",
                    userDb.Gender,
                    userDb.ImageUrl
                };

                return new { userData = new { } };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdateUserProfile(UserProfileViewModel model)
        {
            try
            {
                var userData = await _context.User.Where(t => t.Id == model.Id).FirstOrDefaultAsync();

                if (userData == null) throw new Exception("User not available");

                string newFileName = await UploadFileAsync(userData.Id, userData.ImageName, model.ImageFile);

                userData.FullName = string.IsNullOrEmpty(model.FullName) ? userData.FullName : model.FullName;
                userData.Mobile = string.IsNullOrEmpty(model.Mobile) ? userData.Mobile : model.Mobile;
                userData.BirthDate = string.IsNullOrEmpty(model.BirthDate) ? userData.BirthDate : Convert.ToDateTime(model.BirthDate);
                userData.Gender = string.IsNullOrEmpty(model.Gender) ? userData.Gender : model.Gender;
                userData.ImageUrl = string.IsNullOrEmpty(newFileName) ? userData.ImageUrl : $"{_iConfiguration["AppServerSide:IS4ApplicationUrl"]}{newFileName}";
                userData.ImageName = string.IsNullOrEmpty(newFileName) ? userData.ImageName : newFileName;

                _context.Update(userData);
                _context.SaveChanges();

                object obj = new
                {
                    userData.FullName,
                    userData.Email,
                    BirthDate = userData.BirthDate.HasValue ? Convert.ToDateTime(userData.BirthDate).ToString("dd MMM yyyy") : "",
                    userData.Gender,
                    userData.ImageUrl
                };

                return new { userData = obj };

            }
            catch (Exception ex)
            {
                var err = ex.ToString();
                throw ex;
            }
        }

        private async Task<string> UploadFileAsync(Guid userId, string oldFileName, IFormFile postedFile)
        {
            string newFileName = string.Empty;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\userpic\");
            if (postedFile != null && !string.IsNullOrEmpty(postedFile.FileName))
            {
                var extension = Path.GetExtension(postedFile.FileName);
                if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                {
                    if (postedFile.ContentType == "image/jpeg" || postedFile.ContentType == "image/png")
                    {
                        if (postedFile.Length > 1024000)
                            throw new Exception("File size exceeded. Max file size is 1MB. Your selected file size is " + postedFile.Length / 1000 + ".");

                        if (!string.IsNullOrEmpty(oldFileName)) File.Delete(filePath + oldFileName);

                        newFileName = userId + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + extension;

                        using var fileStream = new FileStream(Path.Combine(filePath, newFileName), FileMode.Create);
                        await postedFile.CopyToAsync(fileStream);
                    }
                    else
                        throw new Exception("Only jpeg, jpg or png images are allowed to upload. Your selected file format is " + postedFile.ContentType + ".");
                }
                else
                    throw new Exception("Only jpeg, jpg or png images are allowed to upload.");

            }
            return newFileName;
        }

        public async Task<CommonResponse> GetUserByIdAsync(Guid id)
        {
            var response = new CommonResponse();
            try
            {
                var dbUser = await _context.User.Where(u => u.Id == id)
                        .Select(t => new UserViewModelFOrUpdate
                        {
                            UserName = t.UserName,
                            Mobile = t.Mobile,
                            Email = t.Email,
                            Country = t.Country,
                            City = t.City,
                          
                            
                        }).FirstOrDefaultAsync();

                response.Status = dbUser != null;
                response.StatusCode = dbUser != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound;
                response.Message = "";
                response.Data = dbUser;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "";
                response.Data = new { };

            }
            return response;
        }
    }
}


















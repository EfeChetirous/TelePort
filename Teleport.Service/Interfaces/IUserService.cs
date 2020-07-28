using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teleport.Model.Models;
using Teleport.Model.Models.ApiResultModels;

namespace Teleport.Service.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(UserRegisterModel user);
        Task<Result> AuthenticateAsync(string userName, string password);
        List<UserModel> GetAllUsers();
    }
}

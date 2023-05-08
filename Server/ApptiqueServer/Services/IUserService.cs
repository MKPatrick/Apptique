using ApptiqueServer.Models;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Bson;

namespace ApptiqueServer.Services
{
    public interface IUserService
    {


        Task CreateNewUser(UserModel userModel);
        Task DeleteUserById(string id);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(string id);
        UserModel GetUsersByUsername(string username);
    }
}

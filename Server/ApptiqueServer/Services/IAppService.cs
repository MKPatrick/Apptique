using ApptiqueServer.Models;
using Microsoft.AspNetCore.Components.Forms;
using MongoDB.Bson;

namespace ApptiqueServer.Services
{
    public interface IAppService
    {
        Task UpdateApp(AppModel appModel);
        Task RemoveAPKPhysically(string apkFile);
        Task CreateNewApp(AppModel appModel);
        Task DeleteAppById(string id);
        Task<List<AppModel>> GetAllApps();
        Task<AppModel> GetAppByID(string id);
        Task<string> CreateAPKPhysically(IBrowserFile apkFile);
    }
}
using ApptiqueServer.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ApptiqueServer.Services
{
    public interface IAppService
    {
        Task UpdateApp(AppModel appModel);
        Task CreateNewApp(AppModel appModel);
        Task DeleteAppById(int id);
        Task<List<AppModel>> GetAllApps();
        Task<AppModel> GetAppByID(int id);
        Task<string> CreateAPKPhysically(IBrowserFile apkFile);
    }
}
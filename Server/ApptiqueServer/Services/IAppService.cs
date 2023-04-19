using ApptiqueServer.Models;

namespace ApptiqueServer.Services
{
    public interface IAppService
    {
        Task CreateNewApp(AppModel appModel);
        Task DeleteAppById(int id);
        Task<List<AppModel>> GetAllApps();
        Task<AppModel> GetAppByID(int id);
    }
}
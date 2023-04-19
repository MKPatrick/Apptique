using ApptiqueServer.Models;

namespace ApptiqueServer.Services
{
    public class AppService : IAppService
    {

        private List<AppModel> _apps = new List<AppModel>();

        public AppService() { }



        public async Task CreateNewApp(AppModel appModel)
        {
            _apps.Add(appModel);
        }

        public async Task<AppModel> GetAppByID(int id)
        {
            return _apps.FirstOrDefault(x => x.ID == id);
        }

        public async Task<List<AppModel>> GetAllApps()
        {
            return _apps;
        }

        public async Task DeleteAppById(int id)
        {
            var app = await GetAppByID(id);
            _apps.Remove(app);
        }


    }
}

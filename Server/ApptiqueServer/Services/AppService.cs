using ApptiqueServer.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace ApptiqueServer.Services
{
    public class AppService : IAppService
    {

        private static List<AppModel> _apps = new List<AppModel>();
        private readonly IWebHostEnvironment env;

        public AppService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public async Task RemoveAPKPhysically(string apkFile)
        {
            File.Delete($"{env.WebRootPath}\\{apkFile}");
        }


        public async Task<string> CreateAPKPhysically(IBrowserFile apkFile)
        {
            string fileName = Guid.NewGuid().ToString();
            Stream stream = apkFile.OpenReadStream(1024 * 100000);
            if (!Directory.Exists($"{env.WebRootPath}\\Apps"))
            {
                Directory.CreateDirectory($"{env.WebRootPath}\\Apps");
            }
            var path = $"{env.WebRootPath}\\Apps\\{fileName}.apk";
            FileStream fs = File.Create(path);
            await stream.CopyToAsync(fs);
            stream.Close();
            fs.Close();
            return $"Apps\\{fileName}.apk";
        }

        public async Task UpdateApp(AppModel appModel)
        {

        }

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

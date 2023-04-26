using ApptiqueClient.Models;
using System.Net.Http.Json;

namespace ApptiqueClient.Services
{
    public class AppService : IDisposable
    {
        private readonly HttpClient _httpClient;

        public AppService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<AppModel>> GetAppInformationsFromServer()
        {
            string s = $"{Consts.ServerBaseURL}api/App/AllApps?secret={Consts.ServerSecret}";

            var result = await _httpClient.GetFromJsonAsync<List<AppModel>>(s);
            result.Reverse();
            return result;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

    }


}

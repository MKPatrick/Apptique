using ApptiqueClient.Models;
using System.Net.Http.Json;

namespace ApptiqueClient.Services
{
    public class AppService
    {
        private readonly HttpClient _httpClient;

        public AppService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<AppModel>> GetAppInformationsFromServer()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AppModel>>($"{Consts.ServerBaseURL}App/AllApps?secret={Consts.ServerSecret}");
            result.Reverse();
            return result;
        }

    }
}

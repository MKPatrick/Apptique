using ApptiqueClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
            var result = await _httpClient.GetFromJsonAsync<List<AppModel>>("");
            result.Reverse();
            return result;
        }

    }
}

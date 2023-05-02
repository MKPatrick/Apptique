using System.Net.Http.Json;
using ApptiqueClient.Models;

namespace ApptiqueClient.Services;

public class AppService
{
    private readonly HttpClient _httpClient;

    public AppService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<AppModel>> GetAppInformationsFromServer()
    {
        var s = $"{Consts.ServerBaseURL}api/App/AllApps?secret={Consts.ServerSecret}";

        var result = await _httpClient.GetFromJsonAsync<List<AppModel>>(s);
        result.Reverse();
        return result;
    }
}
using ApptiqueServer.Config;
using ApptiqueServer.Models;
using ApptiqueServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApptiqueServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;
        private readonly IOptions<SecretModel> _configuration;

        public AppController(IAppService appService,IOptions<SecretModel> configuration)
        {
            this._appService = appService;
            this._configuration = configuration;
        }


        [HttpGet(nameof(AllApps))]

        public async Task<List<AppModel>> AllApps(string secret)
        {
            if(_configuration.Value.AppSecret!=secret)
                return new List<AppModel>();
            return await _appService.GetAllApps();
        }






    }
}

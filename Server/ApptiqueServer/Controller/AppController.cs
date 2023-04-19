using ApptiqueServer.Models;
using ApptiqueServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApptiqueServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;

        public AppController(IAppService appService)
        {
            this._appService = appService;
        }


        [HttpGet(nameof(AllApps))]

        public async Task<List<AppModel>> AllApps()
        {
            return await _appService.GetAllApps();
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApptiqueServer.Config;
using ApptiqueServer.Dtos;
using ApptiqueServer.Models;
using ApptiqueServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Newtonsoft.Json;



namespace ApptiqueServer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;
        private readonly IWebHostEnvironment _env;
        private readonly IOptions<SecretModel> _configuration;

        public static UserModel user = new UserModel();

        public AppController(IAppService appService, IOptions<SecretModel> configuration)
        {
            this._appService = appService;
            this._configuration = configuration;
        }


        [HttpGet(nameof(AllApps))]

        public async Task<List<AppModel>> AllApps(string secret)
        {
            if (_configuration.Value.AppSecret != secret)
                return new List<AppModel>();
            return await _appService.GetAllApps();
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UserModelDto request)
        {
            var userService = new UserService(_env, _configuration);

            var user = new UserModel
            {
                _id = ObjectId.GenerateNewId().ToString(),
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            await userService.CreateNewUser(user);

            return Ok(user);
        }



        [HttpPost("login")]
        public ActionResult<UserModel> Login(UserModelDto request)
        {
            var userService = new UserService(_env, _configuration);

            var existingUser = userService.GetUsersByUsername(request.Username);


            if (existingUser == null) return BadRequest(new { message = "Invalid Username or Password" });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, existingUser.Password))
                return BadRequest(new { message = "Invalid Username or Password" });



            var token = CreateToken(existingUser);

            return Ok(new { Token = token });

        }

        private string CreateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.Value.AppSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost("create")]
        public async Task<ActionResult> CreateUserOnNewDB()
        {
            var userService = new UserService(_env, _configuration);


            var user = new UserModel
            {
                _id = ObjectId.GenerateNewId().ToString(),
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin")
            };
            await userService.CreateNewUser(user);

            return Ok(user);
        }




    }
}

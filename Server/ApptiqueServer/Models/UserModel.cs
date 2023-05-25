using Newtonsoft.Json;

namespace ApptiqueServer.Models
{
    public class UserModel
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}

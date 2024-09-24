using Newtonsoft.Json;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Models
{
    public class LoginModel
    {

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

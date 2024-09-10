using Newtonsoft.Json;


namespace OgmentoAPI.Domain.Authorization.Abstraction.Models
{
    public class UserModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("userRoles")]
        public List<string> UserRoles { get; set; }
    }
}

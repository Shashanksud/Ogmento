using Newtonsoft.Json;


namespace OgmentoAPI.Domain.Authorization.Abstraction.Models
{
    public class UserModel
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("userUid")]
        public Guid UserUid { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("userRole")]
        public string UserRole { get; set; }
        [JsonProperty("validityDays")]
        public int? ValidityDays {  get; set; }
        [JsonProperty("salesCenter")]
        public List<string> UserSalesCenter { get; set; }
    }
}

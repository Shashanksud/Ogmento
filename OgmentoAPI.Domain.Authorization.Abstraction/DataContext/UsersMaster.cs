
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;

namespace OgmentoAPI.Domain.Authorization.Abstractions.DataContext
{
    public class UsersMaster
    {
        public UsersMaster()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }
        public int UserId { get; set; }
        public Guid UserUid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId {  get; set; }
        public string City {  get; set; }
        public UserRoles RoleId {  get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int? ValidityDays { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
        public virtual RolesMaster UserRole { get; set; }
      
    }
}

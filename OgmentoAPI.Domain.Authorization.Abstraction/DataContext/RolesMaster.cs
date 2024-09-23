
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;

namespace OgmentoAPI.Domain.Authorization.Abstractions.DataContext
{
    public partial class RolesMaster
    {
        public UserRoles RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public virtual ICollection<UsersMaster> Users { get; set; }
    }
}

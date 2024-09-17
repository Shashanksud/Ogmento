
namespace OgmentoAPI.Domain.Authorization.Abstraction.DataContext
{
    public partial class RolesMaster
    {
        public RolesMaster()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}



namespace OgmentoAPI.Domain.Authorization.Abstraction.DataContext
{
    public partial class UserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual RolesMaster Role { get; set; }
        public virtual UsersMaster User { get; set; }
    }
}

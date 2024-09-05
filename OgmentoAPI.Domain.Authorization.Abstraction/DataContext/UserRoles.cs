using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Authorization.Abstraction.DataContext
{
    public partial class UserRoles
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual RolesMaster Role { get; set; }
        public virtual UsersMaster User { get; set; }
    }
}

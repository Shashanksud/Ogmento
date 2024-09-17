using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Dto
{
    public class UserDetailsDto
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string UserRole { get; set; }

        public string SalesCenters { get; set; }
        public string KioskName { get; set; }

        public Guid UserUId { get; set; }
    }
}

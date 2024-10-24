
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Dto
{
    public class UserDetailsDto
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string KioskName { get; set; }
		public UserRoles RoleId { get; set; }
		public string PhoneNumber { get; set; }
        public string City { get; set; }
        public int? ValidityDays { get; set; }
        public Guid UserUid { get; set; }

        public Dictionary<Guid, string> SalesCenters{ get; set; }

    }
}


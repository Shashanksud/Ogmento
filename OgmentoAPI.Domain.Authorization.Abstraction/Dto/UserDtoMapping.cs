using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Dto
{
    public static class UserDto
    {
        public static UserDetailsDto ToDto(this UserModel user)
        {

            UserDetailsDto dto = new UserDetailsDto
            {
                UserName = user.UserName,
                UserRole = user.UserRole,
                EmailId = user.Email,
                UserUId = user.UserUid,
                SalesCenters = string.Join(",", user.UserSalesCenter)
            };

            return dto;

        }
    }
}


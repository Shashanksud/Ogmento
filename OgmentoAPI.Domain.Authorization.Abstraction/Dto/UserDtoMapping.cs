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

        public static UserModel ToModel(this UserDetailsDto dto)
        {
            UserModel userModel = new UserModel()
            {
                UserName = dto.UserName,
                UserRole = dto.UserRole,
                Email = dto.EmailId,
                UserUid = dto.UserUId,
                UserSalesCenter = dto.SalesCenters?.Split(',')
                          .Select(s => s.Trim()) // Trim any whitespace
                          .ToList() ?? new List<string>()
            };

            return userModel;

        }
    }
}


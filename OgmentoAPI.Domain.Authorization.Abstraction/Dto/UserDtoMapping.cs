using OgmentoAPI.Domain.Authorization.Abstraction.DataContext;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


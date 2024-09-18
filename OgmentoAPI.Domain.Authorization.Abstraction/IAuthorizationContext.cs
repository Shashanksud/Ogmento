using OgmentoAPI.Domain.Authorization.Abstractions.DataContext;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions
{
    public interface IAuthorizationContext
    {
        UsersMaster GetUserDetail(LoginModel login);
        RolesMaster GetUserRole(int userId);
    }
   
}

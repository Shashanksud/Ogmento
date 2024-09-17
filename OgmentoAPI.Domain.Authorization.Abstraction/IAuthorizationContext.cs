using OgmentoAPI.Domain.Authorization.Abstraction.DataContext;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;

namespace OgmentoAPI.Domain.Authorization.Abstraction
{
    public interface IAuthorizationContext
    {
        UsersMaster GetUserDetail(LoginModel login);

        RolesMaster GetUserRoles(int userID);

        
    }
   
}

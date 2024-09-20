using OgmentoAPI.Domain.Authorization.Abstractions.DataContext;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Repository
{
    public interface IAuthorizationRepository
    {
        UsersMaster GetUserDetail(LoginModel login);
        RolesMaster GetUserRole(int userId);
        UserModel GetUserByID(int userId);
        string GetRoleName(int userId);
    }

}

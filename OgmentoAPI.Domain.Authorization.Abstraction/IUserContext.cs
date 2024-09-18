using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions
{
    public interface IUserContext
    {
        UserModel GetUserByID(int userId);
        string GetRoleName(int userId);
    }
}

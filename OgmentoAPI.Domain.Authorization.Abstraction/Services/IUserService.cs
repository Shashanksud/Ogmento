using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Services
{
    public interface IUserService
    {
        UserModel GetUserDetails(int UserId);
    }
}

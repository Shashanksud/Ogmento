using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions
{
    public interface IUserService
    {
        ResponseModel<UserModel> Get(int userId);

        UserModel GetUserDetails(int UserId);

    }

}

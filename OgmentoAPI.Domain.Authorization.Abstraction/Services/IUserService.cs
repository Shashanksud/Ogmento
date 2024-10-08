using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Services
{
	public interface IUserService
    {
        UserModel GetUserDetail(int userId);
        List<UserModel> GetUserDetails();

        int? UpdateUser(UserModel user);
        int? AddUser(UserModel user);


        bool DeleteUser(Guid userUId);
    }
}

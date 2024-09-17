using OgmentoAPI.Domain.Authorization.Abstraction.Models;



namespace OgmentoAPI.Domain.Authorization.Abstraction
{
    public interface IUserContext
    {
        UserModel GetUserByID(long UserId);
        List<string> GetRoleNames(long UserId);

    }
}

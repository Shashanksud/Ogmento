using OgmentoAPI.Domain.Authorization.Abstraction.Models;



namespace OgmentoAPI.Domain.Authorization.Abstraction
{
    public interface IUserContext
    {
        UserModel GetUserByID(int UserId);
        string GetRoleName(int UserId);

    }
}

using OgmentoAPI.Domain.Authorization.Abstraction.Models;


namespace OgmentoAPI.Domain.Authorization.Abstraction
{
    public interface IIdentityService
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login);
    }
}

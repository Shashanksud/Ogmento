using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions
{
    public interface IIdentityService
    {
        Task<ResponseModel<TokenModel>> LoginAsync(LoginModel login);
    }
}

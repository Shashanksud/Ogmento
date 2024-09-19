using OgmentoAPI.Domain.Authorization.Abstractions.Models;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Services
{
    public interface IIdentityService
    {
        Task<TokenModel> LoginAsync(LoginModel login);
    }
}

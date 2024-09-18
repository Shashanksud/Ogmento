
namespace OgmentoAPI.Domain.Authorization.Abstractions
{
    public interface ICookieService
    {
        void SetAuthToken(string token);
    }
}

namespace OgmentoAPI.Domain.Authorization.Abstractions.Services
{
    public interface ICookieService
    {
        void SetAuthToken(string token);
    }
}

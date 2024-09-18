using Microsoft.AspNetCore.Http;
using OgmentoAPI.Domain.Authorization.Abstractions;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public class CookieService: ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor _httpContextAccessor)
        {
            this._httpContextAccessor = _httpContextAccessor; 
        }
        public void SetAuthToken(string token) {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Response.Cookies.Append("Auth", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddSeconds(2745)
                });
            }
        }
    }
}

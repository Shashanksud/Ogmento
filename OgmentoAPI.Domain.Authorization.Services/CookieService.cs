using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public class CookieService: ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceConfiguration _appSettings;
        public CookieService(IHttpContextAccessor _httpContextAccessor, IOptions<ServiceConfiguration> appSettings)
        {
            _appSettings = appSettings.Value;
            this._httpContextAccessor = _httpContextAccessor; 
        }
        public void SetAuthToken(string token) {
//            var context = _httpContextAccessor.HttpContext;
//            if (context != null)
//            {

//                context.Response.Cookies.Append("Auth", token, new CookieOptions
//                {
//#if DEBUG
//					HttpOnly = false,
//					//Secure = false,
//					SameSite = SameSiteMode.Lax,
//					Expires = DateTime.UtcNow.Add(_appSettings.JwtSettings.TokenLifetime)
//#else
//					HttpOnly = false,
//                    Secure = true,
//                    SameSite = SameSiteMode.None,
//                    Expires = DateTime.UtcNow.Add(_appSettings.JwtSettings.TokenLifetime),
//					Path = "/",
//#endif
//				});
//            }
        }
    }
}

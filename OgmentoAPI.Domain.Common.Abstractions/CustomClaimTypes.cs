

using System.IdentityModel.Tokens.Jwt;

namespace OgmentoAPI.Domain.Common.Abstractions
{
    public static class CustomClaimTypes
    {
        public const string UserId = "UserId";
        public const string UserUid = "UserUid";
        public const string EmailId = "EmailId";
        public const string UserName = "UserName";
        public const string Role = "Role";
        public const string Jti = JwtRegisteredClaimNames.Jti;
    }
}

using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;

namespace OgmentoAPI.Domain.Authorization.Api
{
    public class IdentityController: ControllerBase
    {
        private readonly IUserService _userService;
        private UserModel _user;
        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }
        private int GetId()
        {
            int userId = 0;
            string strUserId = User == null ? string.Empty : User.FindFirst(c => c.Type == "UserId")?.Value;
            int.TryParse(strUserId, out userId);
            return userId;
        }
        protected UserModel Self
        {
            get
            {
                if (_user == null)
                {
                    try
                    {

                        _user = _userService.GetUserDetails(GetId());
                    }
                    catch
                    {
                    }
                }

                return _user;
            }
        }
    }
}

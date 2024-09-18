using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OgmentoAPI.Domain.Authorization.Abstractions.Dto;
using OgmentoAPI.Domain.Authorization.Services;
using OgmentoAPI.Domain.Authorization.Abstractions;
using System.Security.Claims;


namespace OgmentoAPI.Domain.Authorization.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("getCurrentUser")]
        [HttpGet]
        [Authorize(Policy ="AdminPolicy")]
        public IActionResult GetCurrentUser()
        {
            int UserId = GetUserIdFromToken();
            var result = _userService.Get(UserId);
            return Ok(result);
        }

        [Route("GetUserDetails")]
        [HttpGet]
        [Authorize]
        [Produces(typeof(UserDetailsDto))]
        public IActionResult GetUserDetails()
        {
            int UserId = GetUserIdFromToken();
            var result = _userService.GetUserDetails(UserId).ToDto();
          //  var obj= UserDto.ToUserDto(result);
            return Ok(result);
        }

        protected int GetUserIdFromToken()
        {
            int UserId = 0;
            try
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        string strUserId = identity.FindFirst("UserId").Value;
                        int.TryParse(strUserId, out UserId);
                    }
                }
                return UserId;
            }
            catch
            {
                return UserId;
            }
        }

    }
}

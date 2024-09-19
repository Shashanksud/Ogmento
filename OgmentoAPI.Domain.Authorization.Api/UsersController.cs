using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Authorization.Abstractions.Dto;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;


namespace OgmentoAPI.Domain.Authorization.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : IdentityController
    {
        public UsersController(IUserService _userService): base(_userService)//, contextAccessor)
        {

        }
        [Route("getCurrentUser")]
        [HttpGet]
        [Authorize(Policy ="AdminPolicy")]
        public IActionResult GetCurrentUser()
        {
            var result = Self;
            return Ok(result);
        }
        [Route("GetUserDetails")]
        [HttpGet]
        [Authorize]
        [Produces(typeof(UserDetailsDto))]
        public IActionResult GetUserDetails()
        {
            var result = Self.ToDto();
            return Ok(result);
        }
    }
}

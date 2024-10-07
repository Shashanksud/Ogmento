using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OgmentoAPI.Domain.Authorization.Abstractions.Dto;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;
using OgmentoAPI.Domain.Common.Abstractions.Helpers;


namespace OgmentoAPI.Domain.Authorization.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : IdentityController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger): base(userService,logger)//, contextAccessor)
        {
            _userService = userService;
        }
        [Route("getCurrentUser")]
        [HttpGet]
        [Authorize(Policy = PolicyNames.Administrator)]
        public IActionResult GetCurrentUser()
        {
            var result = Self;
            return Ok(result);
        }
        [Route("GetUserDetail")]
        [HttpGet]
        [Authorize]
        [Produces(typeof(UserDetailsDto))]
        public IActionResult GetUserDetail()
        {
            var result = Self.ToDto();
            return Ok(result);
        }

        [Route("getUserDetails")]
        [HttpGet]
        [Authorize]
        [Produces(typeof(List<UserDetailsDto>))]
        public IActionResult GetUserDetails()
        {
            List<UserDetailsDto> result = _userService.GetUserDetails().ToDto();
            return Ok(result);
        }

        [Route("UpdateUserDetails")]
        [HttpPost]
        [Authorize]
        [Produces(typeof(bool))]
        public IActionResult UpdateUser(UserDetailsDto userDetails)
        {
       
            UserModel model = userDetails.ToModel(Self.UserId);
            var result = _userService.UpdateUser(model);
            return Ok(result);
        }
        [Route("AddUser")]
        [HttpPost]
        [Authorize]
        [Produces(typeof(int?))]
        public IActionResult AddUser(AddUserDto user)
        {
            var result = _userService.AddUser(user.ToModel(-1));
            return Ok(result);
        }
        [Route("DeleteUserDetails/{userUId}")]
        [HttpDelete]
        [Authorize]
        public IActionResult DeleteUserDetails(Guid userUId)
        {
            var result = _userService.DeleteUser(userUId);
            return Ok(result);
        }

    }
}

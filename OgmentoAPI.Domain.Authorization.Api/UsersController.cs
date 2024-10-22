using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OgmentoAPI.Domain.Authorization.Abstractions.Dto;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;
using OgmentoAPI.Domain.Common.Abstractions.Helpers;
using System;
using System.Collections.Generic;

namespace OgmentoAPI.Domain.Authorization.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : IdentityController
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService, ILogger<UsersController> logger)
			: base(userService, logger)
		{
			_userService = userService;
		}

		[Route("getCurrentUser")]
		[HttpGet]
		[Authorize(Policy = PolicyNames.Administrator)]
		public IActionResult GetCurrentUser()
		{
			UserDetailsDto result = Self.Adapt<UserDetailsDto>();
			return Ok(result);
		}

		[Route("GetUserDetail")]
		[HttpGet]
		[Authorize]
		[Produces(typeof(UserDetailsDto))]
		public IActionResult GetUserDetail()
		{
			UserDetailsDto result = Self.Adapt<UserDetailsDto>();
			return Ok(result);
		}

		[Route("getUserDetails")]
		[HttpGet]
		[Authorize]
		[Produces(typeof(List<UserDetailsDto>))]
		public IActionResult GetUserDetails()
		{
			List<UserModel> result = _userService.GetUserDetails();
			List<UserDetailsDto> userDetailsDtos = UserMapsterConfig.MapToDto(result);
			return Ok(userDetailsDtos);
		}

		[Route("UpdateUserDetails")]
		[HttpPost]
		[Authorize]
		[Produces(typeof(bool))]
		public IActionResult UpdateUserDetails(UserDetailsDto userDetails)
		{
			UserModel model = userDetails.Adapt<UserModel>();
			model.UserId = Self.UserId;
			var result = _userService.UpdateUser(model);
			return Ok(result);
		}

		[Route("AddUser")]
		[HttpPost]
		[Authorize]
		[Produces(typeof(int?))]
		public IActionResult AddUser(AddUserDto user)
		{
			UserModel model = user.Adapt<UserModel>();
			//model.UserId = Self.UserId;
			var result = _userService.AddUser(model);
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

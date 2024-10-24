using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Client.Abstractions.Dto;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using OgmentoAPI.Domain.Client.Abstractions.Service;

namespace OgmentoAPI.Domain.Client.Api
{

	[ApiController]
	[Authorize]
	[Route("api/[controller]")]
	public class KioskController : ControllerBase
	{
		private readonly IKioskService _kioskService;

		public KioskController(IKioskService kioskService)
		{
			_kioskService = kioskService;
		}

		[HttpGet]
		public IActionResult GetKioskDetails()
		{
			List<KioskModel> result = _kioskService.GetKioskDetails();
			List<KioskDto> response = result.Adapt<List<KioskDto>>();
			return Ok(response);
		}

		[Route("addkiosk")]
		[HttpPost]
		public async Task<IActionResult> AddKiosk(KioskDto kioskdto)
		{
			await _kioskService.AddKiosk(kioskdto.ToModel());
			return Ok();

		}

		[HttpPut]
		[Route("update/{kioskName}/{salesCenterUid}")]
		public async Task<IActionResult> UpdateKioskDetails(string kioskName, Guid salesCenterUid)
		{
			await _kioskService.UpdateKioskDetails(kioskName, salesCenterUid);
			return Ok();
		}

		[Route("delete/{kioskName}")]
		[HttpDelete]
		public async Task<IActionResult> DeleteKiosk(string kioskName)
		{
			await _kioskService.DeleteKioskByName(kioskName);
			return Ok();
		}

		
	}
}





using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Common.Abstractions.Services;

namespace OgmentoAPI.Domain.Common.Api
{
	[ApiController]
	[Route("api/[Controller]")]
    public class PictureController: ControllerBase
    {
		private readonly IPictureService _pictureServices;
		public PictureController(IPictureService pictureServices)
		{
			_pictureServices = pictureServices;
		}
    }
}

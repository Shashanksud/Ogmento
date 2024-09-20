using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using OgmentoAPI.Domain.Client.Abstractions.Service;

namespace OgmentoAPI.Domain.Client.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesCenterController: ControllerBase
    {
        private readonly ISalesCenterService _salesCenterService;
        public SalesCenterController(ISalesCenterService _salesCenterService)
        {
            this._salesCenterService = _salesCenterService;
        }   
        [Route("GetAll")]
        [Authorize]
        [HttpGet]
        public IActionResult GetAllSalesCenters()
        {
            List<SalesCenterModel> response = _salesCenterService.GetAllSalesCenters();
            return Ok(response);
        }
    }
}

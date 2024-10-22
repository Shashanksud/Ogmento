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
    public class SalesCenterController : ControllerBase
    {
        private readonly ISalesCenterService _salesCenterService;
        public SalesCenterController(ISalesCenterService _salesCenterService)
        {
            this._salesCenterService = _salesCenterService;
        }

        [HttpGet]

        public IActionResult GetAllSalesCenters()
        {
            List<SalesCentersDto> response = _salesCenterService.GetAllSalesCenters().ToDto();
            return Ok(response);
        }


        [Route("UpdateMainSalesCenter")]
        [HttpPost]
        public IActionResult UpdateMainSalesCenter(SalesCentersDto salesCentersDto)
        {
            int? response = _salesCenterService.UpdateMainSalesCenter(salesCentersDto.ToModel());
            return Ok(response);
        }

        [HttpPost]
      [Route("AddSalesCenter")]
        public IActionResult AddSalesCenter(SalesCentersDto salesCenterDto)
        {
            var result = _salesCenterService.AddSalesCenter(salesCenterDto);
            if (result.HasValue)
            {
                return Ok(result);
            }
            return BadRequest("Sales center already exists");
        }



        [Route("delete/{salesCenterUid}")]
        [HttpDelete]
        public IActionResult DeleteSalesCenter(Guid salesCenterUid)
        {
            int? Response = _salesCenterService.DeleteSalesCenter(salesCenterUid);
            return Ok(Response);
        }
    }
}

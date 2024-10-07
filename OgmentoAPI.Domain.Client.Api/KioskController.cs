using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Client.Abstractions.Dto;
using OgmentoAPI.Domain.Client.Abstractions.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        [Authorize]
        [HttpGet]
        [Route("GetKioskDetails")]
        public IActionResult GetKioskDetails()
        {
            List<KioskDto> response = _kioskService.GetKioskDetails().ToDto();
            return Ok(response);
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateKioskDetails/{kioskName}/{salesCenterUid}")]
        public IActionResult UpdateKioskDetails(string kioskName, Guid salesCenterUid)
        {
            int? name = _kioskService.UpdateKioskDetails(kioskName, salesCenterUid);

            return Ok(name);
        }

        [Route("delete/{kioskName}")]
        [HttpDelete]
        public IActionResult DeleteKiosk(string kioskName)
        {
            bool response = _kioskService.DeleteKioskByName(kioskName);
            return Ok(response);
        }
    }
}



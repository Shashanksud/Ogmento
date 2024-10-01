using OgmentoAPI.Domain.Client.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Abstractions.Dto
{
    public static class KioskDtoMapping
    {
        public static List<KioskDto> ToDto(this List<KioskModel> kioskModel)
        {
            return kioskModel.Select(kiosk => new KioskDto
            {
                KioskName = kiosk.KioskName,
                SalesCenter = kiosk.SalesCenter,
            }).ToList();
        }


        }
    }


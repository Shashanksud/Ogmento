using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Abstractions.Dto
{
    public class KioskDto
    {
       
        public string KioskName { get; set; }

        public Tuple<Guid, string> SalesCenter { get; set; }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Abstractions.Dto
{
    public class KioskDto
    {
        public int ID { get; set; }

        public string KioskName { get; set; }

        public int SalesCenterId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public Dictionary<Guid, string> SalesCenters { get; set; }

        public Dictionary<Guid, string> SalesCenterUid { get; set; }
    }
}

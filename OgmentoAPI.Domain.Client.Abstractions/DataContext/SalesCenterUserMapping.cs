using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Abstractions.DataContext
{
    public class SalesCenterUserMapping
    {

        public int userID { get; set; }
        public UsersMaster User { get; set; }
        public int SalesCentreID { get; set; }
        public SalesCenter SalesCenters { get; set; }
    }
}

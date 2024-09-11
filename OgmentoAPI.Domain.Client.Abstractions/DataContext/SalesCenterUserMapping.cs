using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Abstractions.DataContext
{
    public class SalesCenterUserMapping
    {
        
        public int UserId { get; set; }
        
        public int SalesCenterId { get; set; }
        public SalesCenter SalesCenters { get; set; }
        //public UsersMaster User {  get; set; }
    }
}

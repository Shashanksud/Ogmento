using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Abstractions.Service
{
    public interface ISalesCenterService
    {
        IEnumerable<string> GetSalesCenterNames(long Id);
    }
}

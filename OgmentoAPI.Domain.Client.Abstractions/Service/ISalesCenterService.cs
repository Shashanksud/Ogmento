using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Models;

namespace OgmentoAPI.Domain.Client.Abstractions.Service
{
    public interface ISalesCenterService
    {
        IEnumerable<SalesCenter> GetSalesCenter(int Id);
        List<SalesCenterModel> GetAllCenters();
    }
}

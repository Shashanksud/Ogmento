using OgmentoAPI.Domain.Client.Abstractions.DataContext;

namespace OgmentoAPI.Domain.Client.Abstractions.Service
{
    public interface ISalesCenterService
    {
        IEnumerable<SalesCenter> GetSalesCenterDetails(int Id);
    }
}

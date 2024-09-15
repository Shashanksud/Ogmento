using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using System.Linq.Expressions;


namespace OgmentoAPI.Domain.Client.Abstractions.Repositories
{
    public interface ISalesCenterRepository
    {
        IEnumerable<SalesCenter> GetSalesCenters(Expression<Func<SalesCenterUserMapping, bool>> predicate);
        

    }
}

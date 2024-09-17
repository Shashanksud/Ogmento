using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using System.Linq.Expressions;


namespace OgmentoAPI.Domain.Client.Abstractions.Repositories
{
    public interface ISalesCenterRepository
    {
        IEnumerable<int> GetSalesCenterIds(Expression<Func<SalesCenterUserMapping, bool>> predicate);
        IEnumerable<SalesCenter> GetSalesCenterDetails(Expression<Func<SalesCenterUserMapping, bool>> predicate);


    }
}

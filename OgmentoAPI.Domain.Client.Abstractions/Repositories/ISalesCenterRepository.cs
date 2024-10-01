using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using System.Linq.Expressions;

namespace OgmentoAPI.Domain.Client.Abstractions.Repositories
{
    public interface ISalesCenterRepository
    {
        IEnumerable<int> GetSalesCenterIds(Expression<Func<SalesCenterUserMapping, bool>> predicate);
        IEnumerable<SalesCenter> GetSalesCenter(Expression<Func<SalesCenterUserMapping, bool>> predicate);

        int? UpdateSalesCentersForUser(int userId, List<Guid> guids);

        List<SalesCenterModel> GetSalesCenterDetails();

        int? UpdateMainSalesCenter(SalesCenterModel salesCenterModel);

        int? DeleteSalesCenter(Guid salesCenterUid);

        int GetUserSalesCenterMappingId(Guid salesCenterUid);
        SalesCenter GetSalesCenterDetail(Guid salesCenterUid);



    }
}

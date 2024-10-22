using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Dto;
using OgmentoAPI.Domain.Client.Abstractions.Models;

namespace OgmentoAPI.Domain.Client.Abstractions.Service
{
    public interface ISalesCenterService
    {
        IEnumerable<SalesCenter> GetSalesCenterForUser(int Id);
        List<SalesCenterModel> GetAllSalesCenters();

        int? UpdateSalesCenters(int userId, List<Guid> guids);

        int? UpdateMainSalesCenter(SalesCenterModel salesCenterModel);
        int? DeleteSalesCenter(Guid salesCenterUid);

        SalesCenter GetSalesCenterDetail(Guid salesCenterUid);

		int? AddSalesCenter(SalesCentersDto salesCenterDto);


	}
}

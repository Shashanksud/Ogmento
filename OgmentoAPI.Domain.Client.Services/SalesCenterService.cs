using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using OgmentoAPI.Domain.Client.Abstractions.Service;
using OgmentoAPI.Domain.Common.Abstractions.Helpers;
using System.Linq.Expressions;
using static OgmentoAPI.Domain.Common.Abstractions.Helpers.Enums;

namespace OgmentoAPI.Domain.Client.Services
{
    public class SalesCenterService : ISalesCenterService
    {
        private readonly ISalesCenterRepository _salesCenterRepository;
        public SalesCenterService(ISalesCenterRepository salesCenterRepository)
        {
            _salesCenterRepository = salesCenterRepository;
        }
        public IEnumerable<SalesCenter> GetSalesCenterForUser(int Id)
        {
            Expression<Func<SalesCenterUserMapping, bool>> predicate = (mapping => mapping.UserId == Id);
            return _salesCenterRepository.GetSalesCenter(predicate);
        }
        public List<SalesCenterModel> GetAllSalesCenters()
        {
            IEnumerable<SalesCenter> salesCenters = _salesCenterRepository.GetSalesCenterDetails();
            List<SalesCenterModel> salesCenterModel = new List<SalesCenterModel>();
            foreach (var centers in salesCenters)
            {
                salesCenterModel.Add(
                    new SalesCenterModel
                    {
                        SalesCenterName = centers.SalesCenterName,
                        SalesCenterUid = centers.SalesCenterUid.ToString(),
                        Country = EnumHelper.GetEnumName<Country>(centers.CountryId),
                        City = centers.City,
                        SalesCenterId = centers.ID
                    });
            }
            return salesCenterModel;
        }
    }
}

using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using OgmentoAPI.Domain.Client.Abstractions.Service;
using OgmentoAPI.Domain.Client.Infrastructure.Repository;
using System.Linq.Expressions;


namespace OgmentoAPI.Domain.Client.Services
{
    public class SalesCenterService :ISalesCenterService
    {
        private readonly ISalesCenterRepository _salesCenterRepository;
        public SalesCenterService(ISalesCenterRepository salesCenterRepository)
        {
            _salesCenterRepository = salesCenterRepository;
            
        }

        public IEnumerable<string> GetSalesCenterNames(long Id)
        {
            Expression<Func<SalesCenterUserMapping, bool>> predicate = (mapping => mapping.UserId == Id);
            var SalesCenter = _salesCenterRepository.GetSalesCenters(predicate);
            return SalesCenter.Select(sc => sc.SalesCenterName);
        }
    }
}

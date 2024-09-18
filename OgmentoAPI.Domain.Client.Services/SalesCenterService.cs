using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using OgmentoAPI.Domain.Client.Abstractions.Service;
using System.Linq.Expressions;

namespace OgmentoAPI.Domain.Client.Services
{
    public class SalesCenterService : ISalesCenterService
    {
        private readonly ISalesCenterRepository _salesCenterRepository;
        public SalesCenterService(ISalesCenterRepository salesCenterRepository)
        {
            _salesCenterRepository = salesCenterRepository;
        }
        public IEnumerable<SalesCenter> GetSalesCenterDetails(int Id)
        {
            Expression<Func<SalesCenterUserMapping, bool>> predicate = (mapping => mapping.UserId == Id);
            return _salesCenterRepository.GetSalesCenterDetails(predicate);
        }
    }
}

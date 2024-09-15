using Microsoft.EntityFrameworkCore;
using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Infrastructure.Repository
{
    public class SalesCenterRepository : ISalesCenterRepository
    {
        private readonly ClientDBContext _Context;
        public SalesCenterRepository(ClientDBContext Context)
        {
            _Context = Context;
        }
        public IEnumerable<SalesCenter> GetSalesCenters(Expression<Func<SalesCenterUserMapping, bool>> predicate)
        {
            return _Context.SalesCenterUserMapping
                .AsNoTracking()
                .Where(predicate)
                .Include(mapping=>mapping.SalesCenter)
                .Select(mapping=> mapping.SalesCenter)
                .ToList();
        }
    }
}

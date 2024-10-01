using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Dto;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Infrastructure.Repository
{
    public class KioskRepository : IKioskRepository
    {
        private readonly ClientDBContext _context;

        public KioskRepository(ClientDBContext context)
        {
            _context = context;
        }



        public List<KioskModel> GetKioskDetails()
        {
            List<Kiosk> allDetail = _context.Kiosk.ToList();

            List<KioskModel> kioskModels = allDetail.Select(x => new KioskModel
            {
                KioskName = x.KioskName,
                SalesCenterId = x.SalesCenterId,
                ID = x.ID,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,

            }).ToList();
            return kioskModels;

        }

        public int? UpdateKioskDetails(string kioskName, int salesCenterId)
        {
            Kiosk kiosk = _context.Kiosk.FirstOrDefault(x => x.KioskName == kioskName);
            kiosk.SalesCenterId = salesCenterId;

            _context.Update(kiosk);
            return _context.SaveChanges();

        }
    }
}

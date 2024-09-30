using OgmentoAPI.Domain.Client.Abstractions.Dto;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Client.Infrastructure.Repository
{
    public class KioskRepository
    {
        private readonly ClientDBContext _context;

        public KioskRepository(ClientDBContext context)
        {
            _context = context;
        }

        public int? GetKioskDetails(KioskDto kioskDto)
        {
          

        }


    }
}

using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using OgmentoAPI.Domain.Common.Abstractions.CustomExceptions;

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

			List<KioskModel> kioskModels = GetKiosk(allDetail);
			return kioskModels;
		}

		public List<KioskModel> GetKioskDetails(List<int> salesCenterIds)
		{
			List<Kiosk> allDetail = _context.Kiosk.Where(x => salesCenterIds.Contains(x.SalesCenterId)).ToList();

			List<KioskModel> kioskModels = GetKiosk(allDetail);
			return kioskModels;

		}

		public async Task UpdateKioskDetails(string kioskName, int salesCenterId)
		{
			Kiosk? kiosk = _context.Kiosk.FirstOrDefault(x => x.KioskName == kioskName);
			if (kiosk == null)
			{
				throw new EntityNotFoundException($"{kioskName} doesn't exist.");
			}
			kiosk.SalesCenterId = salesCenterId;
			_context.Update(kiosk);
			int rowsUpdated = await _context.SaveChangesAsync();
			if (rowsUpdated == 0)
			{
				throw new DatabaseOperationException($"Unable to delete Kiosk {kioskName}");
			}

		}
		public async Task DeleteKioskByName(string kioskName)
		{
			Kiosk? kiosk = _context.Kiosk.FirstOrDefault(x => x.KioskName == kioskName);
			if (kiosk == null)
			{
				throw new EntityNotFoundException($"{kioskName} doesn't exist.");
			}
			_context.Kiosk.Remove(kiosk);
			int rowsDeleted = await _context.SaveChangesAsync();
			if (rowsDeleted == 0)
			{
				throw new DatabaseOperationException($"Unable to delete Kiosk {kioskName}");
			}
		}

		public List<KioskModel> GetKiosk(List<Kiosk> kiosk)
		{

			List<KioskModel> kioskModels = kiosk.Select(x => new KioskModel
			{
				KioskName = x.KioskName,
				SalesCenterId = x.SalesCenterId,
				ID = x.ID,
				IsActive = x.IsActive,
				IsDeleted = x.IsDeleted,

			}).ToList();
			return kioskModels;
		}
		public async Task AddKiosk(KioskModel kioskModel)
		{
			bool isExists = _context.Kiosk.Any(x => x.KioskName == kioskModel.KioskName
	  && x.SalesCenterId == kioskModel.SalesCenterId);
			if (isExists)
			{
				throw new InvalidDataException($"Kiosk Already Exists with name {kioskModel.KioskName}.");
			}
			Kiosk kiosk = new Kiosk()
			{
				KioskName = kioskModel.KioskName,
				SalesCenterId = kioskModel.SalesCenterId,
				IsActive = kioskModel.IsActive,
				IsDeleted = kioskModel.IsDeleted,
			};
			_context.Kiosk.Add(kiosk);
			int rowsAdded =await _context.SaveChangesAsync();
			if (rowsAdded == 0)
			{
				throw new DatabaseOperationException("Unable to add Kiosk.");
			}
		}
	}
}

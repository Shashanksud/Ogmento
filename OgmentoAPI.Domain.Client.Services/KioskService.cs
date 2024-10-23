using OgmentoAPI.Domain.Client.Abstractions.DataContext;
using OgmentoAPI.Domain.Client.Abstractions.Models;
using OgmentoAPI.Domain.Client.Abstractions.Repositories;
using OgmentoAPI.Domain.Client.Abstractions.Service;
using System.Security;

namespace OgmentoAPI.Domain.Client.Services
{
	public class KioskService : IKioskService
	{
		private readonly IKioskRepository _kioskRepository;
		private readonly ISalesCenterService _salesCenterService;


		public KioskService(IKioskRepository kioskRepository, ISalesCenterService salesCenterService)
		{
			_kioskRepository = kioskRepository;
			_salesCenterService = salesCenterService;
		}

		public List<KioskModel> GetKioskDetails()
		{
			List<SalesCenterModel> salesCenters = _salesCenterService.GetAllSalesCenters();
			List<KioskModel> kiosks = _kioskRepository.GetKioskDetails();

			kiosks.ForEach(kiosk =>
			{
				SalesCenterModel salesCenterInfo = salesCenters.First(salesCenter => salesCenter.SalesCenterId == kiosk.SalesCenterId);
				kiosk.SalesCenter = new Tuple<Guid, string>(salesCenterInfo.SalesCenterUid, salesCenterInfo.SalesCenterName);

			});
			return kiosks;
		}
		public List<KioskModel> GetKioskDetails(List<int> salesCenterIds)
		{
			List<SalesCenterModel> salesCenters = _salesCenterService.GetAllSalesCenters();
		
			List<KioskModel> kioskDetailList = _kioskRepository.GetKioskDetails(salesCenterIds);

			kioskDetailList.ForEach(kiosk =>
			{
				SalesCenterModel salesCenterInfo = salesCenters.First(salesCenter => salesCenter.SalesCenterId == kiosk.SalesCenterId);
				kiosk.SalesCenter = new Tuple<Guid, string>(salesCenterInfo.SalesCenterUid, salesCenterInfo.SalesCenterName);

			});
			return kioskDetailList;
		}

		public async Task UpdateKioskDetails(string kioskName, Guid salesCenterUid)
		{
			SalesCenter salesCenter = _salesCenterService.GetSalesCenterDetail(salesCenterUid);
			await _kioskRepository.UpdateKioskDetails(kioskName, salesCenter.ID);
		}
		public async Task  DeleteKioskByName(string kioskName)
		{
			//todo need to delete sales center linked with kioskpzs

			await _kioskRepository.DeleteKioskByName(kioskName);
		}
		public async Task AddKiosk(KioskModel kioskModel)
		{
			kioskModel.SalesCenterId = _salesCenterService.GetSalesCenterDetail(kioskModel.SalesCenter.Item1).ID;
		  	await _kioskRepository.AddKiosk(kioskModel);
		}

	}
}

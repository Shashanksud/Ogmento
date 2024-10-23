using OgmentoAPI.Domain.Client.Abstractions.Models;

namespace OgmentoAPI.Domain.Client.Abstractions.Service
{
	public interface IKioskService
	{
		List<KioskModel> GetKioskDetails();
		List<KioskModel> GetKioskDetails(List<int> salesCenterIds);
		Task UpdateKioskDetails(string kioskName, Guid salesCenterUid);

		Task DeleteKioskByName(string kioskName);
		Task AddKiosk(KioskModel kioskModel);
	}

}

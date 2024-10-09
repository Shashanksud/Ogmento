
using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Common.Abstractions.Services
{
	public interface IPictureService
	{
		public Task<int?> GetProductIdFromHash(string hash);
		public List<PictureModel> GetImagesByPictureId(List<int> pictureIds);
	}
}

using OgmentoAPI.Domain.Common.Abstractions.DataContext;
using OgmentoAPI.Domain.Common.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Repository;
using OgmentoAPI.Domain.Common.Abstractions.Services;

namespace OgmentoAPI.Domain.Common.Services
{
	public class PictureServices: IPictureService
	{
		private readonly IPictureRepository _pictureRepository;
		public PictureServices(IPictureRepository pictureRepository)
		{
			_pictureRepository = pictureRepository;
		}
		public async Task<int?> GetProductIdFromHash(string hash)
		{
			return await _pictureRepository.GetPictureIdFromHash(hash);
		}

		public List<PictureModel> GetImagesByPictureId(List<int> pictureIds)
		{
			List<PictureModel> pictures = _pictureRepository.GetImagesByPictureIds(pictureIds);
			return pictures;
		}
	}
}

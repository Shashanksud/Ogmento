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
		public async Task<int?> GetPictureId(string hash)
		{
			return await _pictureRepository.GetPictureId(hash);
		}

		public List<PictureModel> GetPictures(List<int> pictureIds)
		{
			List<PictureModel> pictures = _pictureRepository.GetPictures(pictureIds);
			return pictures;
		}
		public PictureModel AddPicture(PictureModel picture) { 
			return _pictureRepository.AddPicture(picture);
		}

		public async Task<int> DeletePicture(string? hash)
		{
			 return await _pictureRepository.DeletePicture(hash);
		}

		public async Task DeletePictures(List<int> pictureIds)
		{
			await _pictureRepository.DeletePictures(pictureIds);
		}
	}
}

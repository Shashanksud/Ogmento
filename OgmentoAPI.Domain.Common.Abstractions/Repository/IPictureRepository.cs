using OgmentoAPI.Domain.Common.Abstractions.Models;


namespace OgmentoAPI.Domain.Common.Abstractions.Repository
{
	public interface IPictureRepository
	{
		public Task<int> GetPictureId(string hash);
		public Task<List<PictureModel>> GetPictures(List<int> pictureIds);
		public Task<PictureModel> AddPicture(PictureModel pictureModel);
		public Task<int> DeletePicture(string? hash);
		public Task DeletePictures(List<int> pictureIds);
	}
}

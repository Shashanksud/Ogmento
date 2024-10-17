using OgmentoAPI.Domain.Common.Abstractions.Models;


namespace OgmentoAPI.Domain.Common.Abstractions.Repository
{
	public interface IPictureRepository
	{
		public Task<int?> GetPictureId(string hash);
		public List<PictureModel> GetPictures(List<int> pictureIds);
		public PictureModel AddPicture(PictureModel pictureModel);
		public Task<int> DeletePicture(string? hash);
		public Task DeletePictures(List<int> pictureIds);
	}
}

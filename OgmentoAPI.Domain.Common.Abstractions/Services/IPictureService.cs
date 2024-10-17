
using OgmentoAPI.Domain.Common.Abstractions.Models;
using System.Reflection.Metadata;

namespace OgmentoAPI.Domain.Common.Abstractions.Services
{
	public interface IPictureService
	{
		public Task<int?> GetPictureId(string hash);
		public List<PictureModel> GetPictures(List<int> pictureIds);
		public PictureModel AddPicture(PictureModel picture);
		public Task<int> DeletePicture(string? hash);
		public Task DeletePictures(List<int> pictureIds);
	}
}

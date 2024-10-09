using Microsoft.EntityFrameworkCore;
using OgmentoAPI.Domain.Common.Abstractions.DataContext;
using OgmentoAPI.Domain.Common.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Repository;


namespace OgmentoAPI.Domain.Common.Infrastructure.Repository
{
	public class PictureRepository : IPictureRepository
	{
		private readonly CommonDBContext _dbContext;
		public PictureRepository(CommonDBContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<int?> GetPictureIdFromHash(string hash)
		{
			PictureBinary pictureBinary = await _dbContext.pictureBinaries.FirstOrDefaultAsync(x => x.Hash == hash);
			if (pictureBinary != null) {
				Picture picture = await _dbContext.Pictures.FirstOrDefaultAsync(x => x.Id == pictureBinary.PictureId);
				return picture.Id;
			}
			return null;
		}
		public List<PictureModel> GetImagesByPictureIds(List<int> pictureIds)
		{
			List<Picture> pictures = _dbContext.Pictures.Where(x=>pictureIds.Contains(x.Id)).ToList();
			List<PictureModel> pictureModels = pictures.Select(x => new PictureModel
			{
				Id = x.Id,
				FileName = x.FileName,
				MimeType = x.MimeType,
				Hash = _dbContext.pictureBinaries.FirstOrDefault(b => b.PictureId==x.Id).Hash,
				BinaryData = _dbContext.pictureBinaries.FirstOrDefault(b => b.PictureId == x.Id).BinaryData,
			}).ToList();
			return pictureModels;

		}
	}
}

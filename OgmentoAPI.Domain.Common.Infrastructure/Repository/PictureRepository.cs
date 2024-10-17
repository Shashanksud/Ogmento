using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
		public async Task<int> GetPictureId(string hash)
		{
			PictureBinary pictureBinary = await _dbContext.PictureBinary.FirstOrDefaultAsync(x => x.Hash == hash);
			if (pictureBinary == null) {
				throw new InvalidOperationException("Picture not found.");
			}
			return pictureBinary.PictureId;
		}
		public async Task<List<PictureModel>> GetPictures(List<int> pictureIds)
		{
			List<Picture> pictures = _dbContext.Pictures.Where(x=>pictureIds.Contains(x.PictureID)).ToList();
			return pictures.Select(x => new PictureModel
			{
				Id = x.PictureID,
				FileName = x.FileName,
				MimeType = x.MimeType,
				Hash = _dbContext.PictureBinary.FirstOrDefault(b => b.PictureId==x.PictureID).Hash,
				BinaryData = _dbContext.PictureBinary.FirstOrDefault(b => b.PictureId == x.PictureID).BinaryData,
			}).ToList();

		}
		public async Task<PictureModel> AddPicture(PictureModel pictureModel)
		{
			Picture picture = new Picture()
			{
				FileName = pictureModel.FileName,
				MimeType = pictureModel.MimeType,
				AltAttribute = pictureModel.FileName,
				TitleAttribute = pictureModel.FileName
			};
			EntityEntry<Picture> pictureEntity = _dbContext.Pictures.Add(picture);
			await _dbContext.SaveChangesAsync();
			PictureBinary pictureBinary = new PictureBinary()
			{
				PictureId = pictureEntity.Entity.PictureID,
				BinaryData =pictureModel.BinaryData,
			};
			EntityEntry<PictureBinary> pictureBinaryEntity = _dbContext.PictureBinary.Add(pictureBinary);
			await _dbContext.SaveChangesAsync();
			pictureModel.Hash = pictureBinaryEntity.Entity.Hash;
			pictureModel.Id = pictureEntity.Entity.PictureID;
			return pictureModel;
		}
		public async Task<int> DeletePicture(string? hash)
		{
			if(hash == null)
			{
				throw new InvalidOperationException("hash cannot be null");
			}
			int? pictureId = await GetPictureId(hash);
			if (pictureId == null)
			{
				throw new InvalidOperationException("Image cannot be found.");
			}
			Picture picture = _dbContext.Pictures.First(x => x.PictureID == pictureId);
			PictureBinary pictureBinary = _dbContext.PictureBinary.First(x => x.PictureId == pictureId);
			_dbContext.PictureBinary.Remove(pictureBinary);
			_dbContext.Pictures.Remove(picture);
			int response = await _dbContext.SaveChangesAsync();
			return response;
		}

		public async Task DeletePictures(List<int> pictureIds)
		{
			await _dbContext.Pictures.Where(x => pictureIds.Contains(x.PictureID)).ExecuteDeleteAsync();
		}
	}
}

using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Common.Abstractions.Dto
{
	public static class PictureDtoMapping
	{
		public static PictureDto ToDto(this PictureModel picture)
		{
			return new PictureDto()
			{
				BinaryData = picture.BinaryData,
				FileName = picture.FileName,
				MimeType = picture.MimeType,
				Hash = picture.Hash,
			};
		}
		public static PictureModel ToModel(this PictureDto picture)
		{
			return new PictureModel()
			{
				BinaryData = picture.BinaryData,
				FileName = picture.FileName,
				MimeType = picture.MimeType,
				Hash = picture.Hash,
			};
		}
	}
}

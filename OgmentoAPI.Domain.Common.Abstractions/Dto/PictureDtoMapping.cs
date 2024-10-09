using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Common.Abstractions.Dto
{
	public static class PictureDtoMapping
	{
		public static PictureDto ToDto(this PictureModel picture)
		{
			PictureDto pictureDto = new PictureDto()
			{
				BinaryData = picture.BinaryData,
				FileName = picture.FileName,
				MimeType = picture.MimeType,
				Hash = picture.Hash,
			};
			return pictureDto;
		}
		public static PictureModel ToModel(this PictureDto picture)
		{
			PictureModel pictureModel = new PictureModel()
			{
				BinaryData = picture.BinaryData,
				FileName = picture.FileName,
				MimeType = picture.MimeType,
				Hash = picture.Hash,
			};
			return pictureModel;
		}
	}
}

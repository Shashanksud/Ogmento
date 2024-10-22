using CsvHelper.Configuration;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public class UploadPictureModelMap: ClassMap<UploadPictureModel>
	{
		public UploadPictureModelMap() {
			Map(x => x.FileName).Name("Filename");
			Map(x => x.MimeType).Name("FileType");
			Map(x => x.SkuCode).Name("ProductSku");
			Map(x => x.Base64EncodedData).Name("Base64String");
		}
	}
}

namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class UploadPictureModel
	{
		public string SkuCode {  get; set; }
		public string FileName {  get; set; }
		public string Base64EncodedData { get; set; }
		public string MimeType { get; set; }
	}
}

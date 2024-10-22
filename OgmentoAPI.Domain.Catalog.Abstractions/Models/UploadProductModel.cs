
namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class UploadProductModel : ProductBase
	{
		public List<int> CategoryIds { get; set; }
	}
}

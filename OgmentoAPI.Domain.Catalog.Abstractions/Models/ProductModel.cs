using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class ProductModel: ProductBase
	{
		public int ProductId { get; set; }
	
		public List<PictureModel> Images { get; set; }
		public CategoryModel Category { get; set; }
	}
}

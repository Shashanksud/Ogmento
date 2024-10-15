using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Repository
{
	public interface IProductRepository
	{
		public List<ProductModel> GetAllProducts();
		public List<PictureModel> GetImages(int productId);
	}
}

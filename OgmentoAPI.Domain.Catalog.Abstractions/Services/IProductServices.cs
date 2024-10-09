using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Services
{
	public interface IProductServices
	{
		public List<ProductModel> GetAllProducts();
	}
}

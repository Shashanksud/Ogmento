using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;

namespace OgmentoAPI.Domain.Catalog.Services
{
	public class ProductServices: IProductServices
	{
		private readonly IProductRepository _productRepository;
		public ProductServices(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public List<ProductModel> GetAllProducts()
		{
			return _productRepository.GetAllProducts();
		}
	}
}

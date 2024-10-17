using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Repository
{
	public interface IProductRepository
	{
		public List<ProductModel> GetAllProducts();
		public List<PictureModel> GetImages(int productId);
		public ProductModel GetProduct(string sku);
		public Task<ProductModel> UpdateProduct(ProductModel productModel);
		public Task DeleteProduct(string sku);
		public Task<ProductModel> AddProduct(ProductModel productModel);
		public Task<List<ProductModel>> UploadProducts(List<ProductModel> products);
	}
}

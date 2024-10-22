using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Repository
{
	public interface IProductRepository
	{
		public Task<List<ProductModel>> GetAllProducts();
		public Task<List<PictureModel>> GetImages(int productId);
		public Task<ProductModel> GetProduct(string sku);
		public Task UpdateProduct(AddProductModel productModel);
		public Task DeleteProduct(string sku);
		public Task AddProduct(AddProductModel productModel);
		public Task UploadProducts(List<UploadProductModel> products);
		public Task UploadPictures(List<UploadPictureModel> pictures);
	}
}

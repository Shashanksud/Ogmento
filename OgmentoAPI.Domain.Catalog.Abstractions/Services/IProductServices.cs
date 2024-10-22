using Microsoft.AspNetCore.Http;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Services
{
	public interface IProductServices
	{
		public Task<List<ProductModel>> GetAllProducts();
		public Task<ProductModel> GetProduct(string sku);
		public Task UpdateProduct(AddProductModel product);
		public Task DeleteProduct(string sku);
		public Task<ProductModel> AddProduct(AddProductModel product);
		public Task UploadProducts(IFormFile csvFile);
		public Task UploadPictures(IFormFile csvFile);
	}
}

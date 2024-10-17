using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;
using System.Collections.Generic;

namespace OgmentoAPI.Domain.Catalog.Services
{
	public class ProductServices: IProductServices
	{
		private readonly IProductRepository _productRepository;
		public ProductServices(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task<ProductModel> AddProduct(ProductModel product)
		{
			return await _productRepository.AddProduct(product);
		}

		public async Task DeleteProduct(string sku)
		{
			await _productRepository.DeleteProduct(sku);
		}

		public List<ProductModel> GetAllProducts()
		{
			return _productRepository.GetAllProducts();
		}

		public ProductModel GetProduct(string sku)
		{
			return _productRepository.GetProduct(sku);
		}

		public async Task<ProductModel> UpdateProduct(ProductModel product)
		{
			return await _productRepository.UpdateProduct(product);
		}
		public async Task<List<ProductModel>> UploadProducts(List<ProductModel> products)
		{
			return await _productRepository.UploadProducts(products);
		}
	}
}

﻿using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Services
{
	public interface IProductServices
	{
		public Task<List<ProductModel>> GetAllProducts();
		public Task<ProductModel> GetProduct(string sku);
		public Task<ProductModel> UpdateProduct(ProductModel product);
		public Task DeleteProduct(string sku);
		public Task<ProductModel> AddProduct(ProductModel product);
		public Task<List<ProductModel>> UploadProducts(List<ProductModel> products);
	}
}

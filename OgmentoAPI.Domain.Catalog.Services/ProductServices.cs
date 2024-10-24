﻿using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;
using OgmentoAPI.Domain.Catalog.Services.Shared;
using System.Globalization;

namespace OgmentoAPI.Domain.Catalog.Services
{
	public class ProductServices: IProductServices
	{
		private readonly IProductRepository _productRepository;
		public ProductServices(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public async Task<ProductModel> AddProduct(AddProductModel product)
		{
			await _productRepository.AddProduct(product);
			return (await _productRepository.GetProduct(product.SkuCode));
		}
		public async Task DeleteProduct(string sku)
		{
			await _productRepository.DeleteProduct(sku);
		}
		public async Task<List<ProductModel>> GetAllProducts()
		{
			return await _productRepository.GetAllProducts();
		}
		public async Task<ProductModel> GetProduct(string sku)
		{
			return await _productRepository.GetProduct(sku);
		}
		public async Task UpdateProduct(AddProductModel product)
		{
			await _productRepository.UpdateProduct(product);
		}

		

		public async Task UploadPictures(IFormFile csvFile)
		{
			await CatalogHelper.UploadCsvFile<UploadPictureModel, UploadPictureModelMap>(csvFile, _productRepository.UploadPictures);
		}

		public async Task UploadProducts(IFormFile csvFile)
		{
			await CatalogHelper.UploadCsvFile<UploadProductModel, UploadProductModelMap>(csvFile, _productRepository.UploadProducts);
		}
	}
}

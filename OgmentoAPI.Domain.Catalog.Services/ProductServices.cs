using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;
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
			CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = ","
			};
			using (StreamReader csvStreamReader = new StreamReader(csvFile.OpenReadStream()))
			using (CsvReader csvReader = new CsvReader(csvStreamReader, csvConfig))
			{
				csvReader.Context.RegisterClassMap<UploadPictureModelMap>();
				List<UploadPictureModel> pictures = csvReader.GetRecords<UploadPictureModel>().ToList();
				await _productRepository.UploadPictures(pictures);
			}
		}

		public async Task UploadProducts(IFormFile csvFile)
		{
			CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = ";"
			};
			using (StreamReader csvStreamReader = new StreamReader(csvFile.OpenReadStream()))
			using (CsvReader csvReader = new CsvReader(csvStreamReader, csvConfig))
			{
				csvReader.Context.RegisterClassMap<UploadProductModelMap>();
				List<UploadProductModel> products = csvReader.GetRecords<UploadProductModel>().ToList();
				await _productRepository.UploadProducts(products); 
			}
		}
	}
}

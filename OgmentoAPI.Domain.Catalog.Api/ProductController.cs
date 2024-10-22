using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;
using OgmentoAPI.Domain.Common.Abstractions;
using System.Resources;


namespace OgmentoAPI.Domain.Catalog.Api
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class ProductController:ControllerBase
	{
		private readonly IProductServices _productServices;
		private readonly string _productSampleCsv;
		public ProductController(IProductServices productServices, IOptions<FilePaths> filePaths)
		{
			_productSampleCsv = filePaths.Value.ProductSampleCsv;
			_productServices = productServices;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			return Ok((await _productServices.GetAllProducts()).ToDto());
		}
		[HttpGet]
		[Route("{sku}")]
		public async Task<IActionResult> GetProduct(string sku)
		{
			if (string.IsNullOrEmpty(sku))
			{
				throw new InvalidDataException("sku cannot be null or empty.");
			}
			return Ok((await _productServices.GetProduct(sku)).ToDto());
		}
		[HttpPut]
		public async Task<IActionResult> UpdateProduct(AddProductDto productDto)
		{
			await _productServices.UpdateProduct(productDto.ToModel());
			return Ok();
		}
		[HttpDelete]
		[Route("{sku}")]
		public async Task<IActionResult> DeleteProduct(string sku)
		{
			if (string.IsNullOrEmpty(sku)) {
				throw new InvalidDataException("sku cannot be null or empty.");
			}
			await _productServices.DeleteProduct(sku);
			return Ok();
		}
		[HttpPost]
		public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
		{
			return Ok((await _productServices.AddProduct(addProductDto.ToModel())).ToDto());
		}
		[HttpPost]
		[Route("csv")]
		public async Task<IActionResult> UploadProducts(IFormFile file)
		{
			if (file != null && file.Length > 0) {
				await _productServices.UploadProducts(file);
				return Ok();
			}
			else
			{
				throw new InvalidOperationException("The uploaded file is either null or empty. Please upload a valid CSV file.");
			}
		}
		[HttpPost]
		[Route("picture/csv")]
		public async Task<IActionResult> UploadPictures(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				await _productServices.UploadPictures(file);
				return Ok();
			}
			else
			{
				throw new InvalidOperationException("The uploaded file is either null or empty. Please upload a valid CSV file.");
			}
		}
		[HttpGet]
		[Route("SampleCsv")]
		public async Task<IActionResult> DownloadCsvProductSample()
		{
			if (!System.IO.File.Exists(_productSampleCsv))
			{
				return NotFound("Sample Csv File not found");
			}

			byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(_productSampleCsv);
			string fileName = Path.GetFileName(_productSampleCsv);

			return File(fileBytes, "text/csv", fileName);
		}

	}
}

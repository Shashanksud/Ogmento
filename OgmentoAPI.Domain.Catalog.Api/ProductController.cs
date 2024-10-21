using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;


namespace OgmentoAPI.Domain.Catalog.Api
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class ProductController:ControllerBase
	{
		private readonly IProductServices _productServices;
		public ProductController(IProductServices productServices)
		{
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
			return Ok((await _productServices.GetProduct(sku)).ToDto());
		}
		[HttpPut]
		public async Task<IActionResult> UpdateProduct(ProductDto productDto)
		{
			return Ok((await _productServices.UpdateProduct(productDto.ToModel())).ToDto());
		}
		[HttpDelete]
		[Route("{sku}")]
		public async Task<IActionResult> DeleteProduct(string sku)
		{
			await _productServices.DeleteProduct(sku);
			return Ok();
		}
		[HttpPost]
		public async Task<IActionResult> PostProduct(ProductDto productDto)
		{
			return Ok((await _productServices.AddProduct(productDto.ToModel())).ToDto());
		}
		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> UploadProducts(List<ProductDto> products)
		{
			
			return Ok((await _productServices.UploadProducts(products.ToModel())).ToDto());
		}
	}
}

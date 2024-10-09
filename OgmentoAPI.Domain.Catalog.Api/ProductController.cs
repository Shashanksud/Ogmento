using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;


namespace OgmentoAPI.Domain.Catalog.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController:ControllerBase
	{
		private readonly IProductServices _productServices;
		public ProductController(IProductServices productServices)
		{
			_productServices = productServices;
		}
		[HttpGet]
		[Route("GetAllProducts")]
		public IActionResult GetAllProducts()
		{
			return Ok(_productServices.GetAllProducts().Select(x=>x.ToDto()).ToList());
		}
		[HttpGet]
		[Route("GetProductDetails")]
		public IActionResult GetProduct(string sku)
		{
			return Ok();
		}
		[HttpPut]
		[Route("UpdateProductDetails")]
		public IActionResult UpdateProduct(ProductDto productDto)
		{
			return Ok();
		}
		[HttpDelete]
		[Route("DeleteProduct")]
		public IActionResult DeleteProduct(string sku)
		{
			return Ok();
		}
		[HttpPost]
		[Route("AddNewProduct")]
		public IActionResult PostProduct(ProductDto productDto)
		{
			return Ok();
		}
	}
}

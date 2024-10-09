using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Dto;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public static class ProductDtoMapping
	{
		public static ProductDto ToDto(this ProductModel product)
		{
			ProductDto productDto = new ProductDto()
			{
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				Weight = product.Weight,
				LoyaltyPoints = product.LoyaltyPoints,
				ProductExpiry = product.ProductExpiry,
				SkuCode = product.SkuCode,
				Categories = product.Categories.Select(CategoryDtoMapping.ToDto).ToList(),
				Images = product.Images != null ? product.Images.Select(PictureDtoMapping.ToDto).ToList() : [],
			};
			return productDto;
		}
		public static ProductModel ToModel(this ProductDto product, int productId)
		{
			ProductModel productModel = new ProductModel()
			{
				ID = productId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				Weight = product.Weight,
				LoyaltyPoints = product.LoyaltyPoints,
				ProductExpiry = product.ProductExpiry,
				SkuCode = product.SkuCode,
				Categories = product.Categories.Select(CategoryDtoMapping.ToModel).ToList(),
				Images = product.Images.Select(PictureDtoMapping.ToModel).ToList(),
			};
			return productModel;
		}
	}
}

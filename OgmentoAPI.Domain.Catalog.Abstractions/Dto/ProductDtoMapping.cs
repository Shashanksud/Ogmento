using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Dto;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public static class ProductDtoMapping
	{
		public static ProductDto ToDto(this ProductModel product)
		{
			return new ProductDto()
			{
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				Weight = product.Weight,
				LoyaltyPoints = product.LoyaltyPoints,
				ProductExpiry = product.ExpiryDate,
				SkuCode = product.SkuCode,
				Categories = product.Categories?.Select(x => x.ToDto()).ToList() ?? [],
				Images = product.Images?.Select(x=>x.ToDto()).ToList() ?? [],
			};
		}
		public static ProductModel ToModel(this ProductDto product, int productId)
		{
			return new ProductModel()
			{
				ProductId = productId,
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				Weight = product.Weight,
				LoyaltyPoints = product.LoyaltyPoints,
				ExpiryDate = product.ProductExpiry,
				SkuCode = product.SkuCode,
				Categories = product.Categories?.Select(x=> x.ToModel()).ToList() ?? [],
				Images = product.Images?.Select(x => x.ToModel()).ToList() ?? [],
			};
		}
	}
}

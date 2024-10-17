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
				Category = product.Category.ToDto() ,
				Images = product.Images?.Select(x=>x.ToDto()).ToList() ?? [],
			};
		}
		public static ProductModel ToModel(this ProductDto product)
		{
			return new ProductModel()
			{
				ProductName = product.ProductName,
				ProductDescription = product.ProductDescription,
				Price = product.Price,
				Weight = product.Weight,
				LoyaltyPoints = product.LoyaltyPoints,
				ExpiryDate = product.ProductExpiry,
				SkuCode = product.SkuCode,
				Category = product.Category.ToModel(),
				Images = product.Images?.Select(x => x.ToModel()).ToList() ?? [],
			};
		}
		public static List<ProductDto> ToDto(this List<ProductModel> products)
		{
			return products.Select(x => x.ToDto()).ToList();
		}
		public static List<ProductModel> ToModel(this List<ProductDto> products)
		{
			return products.Select(x => x.ToModel()).ToList();
		}
	}
}

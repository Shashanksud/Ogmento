using OgmentoAPI.Domain.Common.Abstractions.Dto;


namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public class ProductDto
	{
		public string SkuCode { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; } = string.Empty;
		public int Price { get; set; }
		public int Weight { get; set; }
		public int? LoyaltyPoints { get; set; }
		public DateOnly ProductExpiry { get; set; }
		public List<PictureDto> Images { get; set; }
		public CategoryDto Category { get; set; }
	}
}

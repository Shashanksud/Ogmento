namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class ProductBase
	{
		public DateOnly ExpiryDate { get; set; }
		public int LoyaltyPoints { get; set; } = 0;
		public int Price { get; set; }
		public string ProductDescription { get; set; } = string.Empty;
		public string ProductName { get; set; }
		public string SkuCode { get; set; }
		public int Weight { get; set; }
	}
}
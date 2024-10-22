
namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class UploadProductModel
	{
		public string SkuCode { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; } = string.Empty;
		public int Price { get; set; }
		public int LoyaltyPoints { get; set; } = 0;
		public int Weight { get; set; }
		public DateOnly ExpiryDate { get; set; }
		public List<int> CategoryIds { get; set; }
	}
}

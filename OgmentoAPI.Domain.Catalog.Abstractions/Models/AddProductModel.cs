

using OgmentoAPI.Domain.Common.Abstractions.Dto;
using OgmentoAPI.Domain.Common.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class AddProductModel
	{
		public int ProductId { get; set; }
		public string SkuCode { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; } = string.Empty;
		public int Price { get; set; }
		public int Weight { get; set; }
		public int? LoyaltyPoints { get; set; }
		public DateOnly ProductExpiry { get; set; }
		public List<PictureModel> Images { get; set; }
		public List<Guid> Categories { get; set; }
	}
}

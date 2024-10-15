using System.ComponentModel.DataAnnotations;

namespace OgmentoAPI.Domain.Catalog.Abstractions.DataContext
{
    public class Product
    {
		[Key]
		public int ProductID { get; set; }
		public string SkuCode { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; } = string.Empty; 
		public int Price {  get; set; }
		public int Weight { get; set; }
		public int? LoyaltyPoints {  get; set; }
		public DateOnly ExpiryDate {  get; set; }
		public bool IsDeleted { get; set; }
		public List<ProductCategoryMapping> ProductCategories { get; set; }
    }
}

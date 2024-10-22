namespace OgmentoAPI.Domain.Catalog.Abstractions.Models
{
	public class CategoryModel
	{
		public int CategoryId { get; set; }
		public Guid CategoryUid { get; set; }
		public string CategoryName { get; set; }
		public int? ParentCategoryId { get; set; }
		public Guid ParentCategoryUid { get; set; }
		public List<CategoryModel> SubCategories { get; set; }
	}
}

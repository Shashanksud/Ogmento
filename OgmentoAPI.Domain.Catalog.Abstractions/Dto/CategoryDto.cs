
namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public class CategoryDto
	{
		public Guid CategoryUid { get; set; }
		public string CategoryName { get; set; }
		public Guid ParentCategoryUid { get; set; }
		public List<CategoryDto> SubCategories { get; set; }
	}
}

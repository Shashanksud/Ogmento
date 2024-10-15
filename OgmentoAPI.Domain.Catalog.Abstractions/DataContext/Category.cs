using System.ComponentModel.DataAnnotations;

namespace OgmentoAPI.Domain.Catalog.Abstractions.DataContext
{
    public class Category
    {
		[Key]
		public int CategoryID { get; set; }
		public Guid CategoryUid { get; set; }
		public string CategoryName { get; set; }
		public int? ParentCategoryId { get; set; }
		public Category ParentCategory { get; set; }
		public List<Category> SubCategories { get; set; }
		public bool IsDeleted { get; set; }
		public List<ProductCategoryMapping> ProductCategories {  get; set; }
    }
}

using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Repository
{
	public interface ICategoryRepository
	{
		public Task<int?> GetCategoryId(Guid Uid);
		public List<Category> GetAllParentCategories();
		public List<CategoryModel> GetSubCategories(int categoryId);
		public CategoryModel GetCategory(int? categoryId);
		public Task DeleteCategory(int? categoryId);
		public Task UpdateCategory(Guid uid, string categoryName);
		public Task<CategoryModel> AddCategory(CategoryModel categoryModel);
		public Task<Guid> GetParentUid(int? parentId);
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel);
	}
}

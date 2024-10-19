using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Repository
{
	public interface ICategoryRepository
	{
		public Task<int> GetCategoryIdAsync(Guid categoryUid);
		public List<CategoryModel> GetSubCategories(int categoryId);
		public Task<CategoryModel> GetCategory(Guid categoryUid);
		public Task DeleteCategory(Guid categoryUid);
		public Task UpdateCategory(Guid categoryUid, string categoryName);
		public Task<CategoryModel> AddCategory(CategoryModel categoryModel);
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel);
		public Task<List<CategoryModel>> GetAllCategories();
		public Guid GetCategoryUid(int? categoryId);
	}
}

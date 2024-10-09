using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Services
{
	public interface ICategoryServices
	{
		public Task<int?> GetCategoryIdFromCategoryUid(Guid Uid);
		public List<CategoryModel> GetAllCategories();
		public CategoryModel GetCategory(Guid categoryUid);
		public Task DeleteCategory(Guid uid);
		public Task UpdateCategory(Guid uid, string categoryName);
		public Task<List<CategoryModel>> AddCategories(List<CategoryModel> categories);
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel);
	}
}

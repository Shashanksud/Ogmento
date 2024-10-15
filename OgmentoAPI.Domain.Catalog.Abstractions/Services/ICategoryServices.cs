using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Services
{
	public interface ICategoryServices
	{
		public Task<int?> GetCategoryIdFromUid(Guid categoryUid);
		public List<CategoryModel> GetAllCategories();
		public CategoryModel GetCategory(Guid categoryUid);
		public Task DeleteCategory(Guid categoryUid);
		public Task UpdateCategory(Guid categoryUid, string categoryName);
		public Task<List<CategoryModel>> AddCategories(List<CategoryModel> categories);
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel);
	}
}

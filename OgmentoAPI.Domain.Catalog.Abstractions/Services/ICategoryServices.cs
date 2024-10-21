using OgmentoAPI.Domain.Catalog.Abstractions.Models;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Services
{
	public interface ICategoryServices
	{
		public Task<int> GetCategoryId(Guid categoryUid);
		public Task<List<CategoryModel>> GetAllCategories();
		public Task<CategoryModel> GetCategory(Guid categoryUid);
		public Task DeleteCategory(Guid categoryUid);
		public Task UpdateCategory(Guid categoryUid, string categoryName);
		public Task<List<CategoryModel>> AddCategories(List<CategoryModel> categories);
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel);
		public Guid GetCategoryUid(int? categoryId);
		public Task<CategoryModel> GetCategoryForProduct(Guid categoryUid);
	}
}

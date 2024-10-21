using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;

namespace OgmentoAPI.Domain.Catalog.Services
{
	public class CategoryServices: ICategoryServices
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryServices(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}
		public Guid GetCategoryUid(int? categoryId)
		{
			return _categoryRepository.GetCategoryUid(categoryId);
		}
		public async Task<int> GetCategoryId(Guid categoryUid)
		{
			return await _categoryRepository.GetCategoryIdAsync(categoryUid);
		}
		public async Task<List<CategoryModel>> GetAllCategories()
		{
			return await _categoryRepository.GetAllCategories();
		}
		public async Task<CategoryModel> GetCategory(Guid categoryUid)
		{
			return await _categoryRepository.GetCategory(categoryUid);
			
		}

		public async Task DeleteCategory(Guid categoryUid)
		{	
			await _categoryRepository.DeleteCategory(categoryUid);
		}
		public async Task UpdateCategory(Guid categoryUid, string categoryName)
		{
			await _categoryRepository.UpdateCategory(categoryUid, categoryName);
		}

		public async Task<List<CategoryModel>> AddCategories(List<CategoryModel> categories)
		{
			List<CategoryModel> addedCategories = new List<CategoryModel>();

			foreach (var category in categories)
			{
				var addedCategory = await _categoryRepository.AddCategory(category);
				addedCategories.Add(addedCategory);
			}

			return addedCategories;
		}
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel)
		{
			return _categoryRepository.AddNewCategory(categoryModel);
		}
		public async Task<CategoryModel> GetCategoryForProduct(Guid categoryUid)
		{
			return await _categoryRepository.GetCategoryForProduct(categoryUid);
		}
	}
}

using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
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
		public async Task<int?> GetCategoryIdFromUid(Guid categoryUid)
		{
			return await _categoryRepository.GetCategoryIdFromUid(categoryUid);
		}
		public List<CategoryModel> GetAllCategories()
		{
			List<Category> parentCategories = _categoryRepository.GetAllParentCategories();
			List<CategoryModel> categoryModel = parentCategories.Select(x => new CategoryModel
			{
				CategoryName = x.CategoryName,
				CategoryId = x.CategoryID,
				ParentCategoryId = x.ParentCategoryId,
				CategoryUid = x.CategoryUid,
				ParentCategoryUid = _categoryRepository.GetParentGuid(x.ParentCategoryId).Result,
				SubCategories = _categoryRepository.GetSubCategories(x.CategoryID),

			}).ToList();
			return categoryModel;
		}
		public CategoryModel GetCategory(Guid categoryUid)
		{
			int? categoryId = GetCategoryIdFromUid(categoryUid).Result;
			if (categoryId == null)
			{
				throw new InvalidOperationException("Category doesn't exist in database.");
			}
			CategoryModel category = _categoryRepository.GetCategory(categoryId);
			category.ParentCategoryUid = new Guid();
			return category;
		}

		public async Task DeleteCategory(Guid categoryUid)
		{
			int? categoryId = GetCategoryIdFromUid(categoryUid).Result;
			if (categoryId == null)
			{
				throw new InvalidOperationException("Category doens't exist.");
			}
			await _categoryRepository.DeleteCategory(categoryId);
		}
		public Task UpdateCategory(Guid categoryUid, string categoryName)
		{
			return _categoryRepository.UpdateCategory(categoryUid, categoryName);
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
	}
}

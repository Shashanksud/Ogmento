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
		public async Task<int?> GetCategoryIdFromCategoryUid(Guid Uid)
		{
			return await _categoryRepository.GetCategoryIdFromCategoryUid(Uid);
		}
		public List<CategoryModel> GetAllCategories()
		{
			List<Category> parentCategories = _categoryRepository.GetAllParentCategories();
			List<CategoryModel> categoryModel = parentCategories.Select(x => new CategoryModel
			{
				CategoryName = x.CategoryName,
				ID = x.ID,
				ParentCategoryId = x.ParentCategoryId,
				CategoryUid = x.CategoryUid,
				ParentCategoryUid = new Guid(),
				SubCategories = _categoryRepository.GetSubCategoriesByCategoryId(x.ID),

			}).ToList();
			return categoryModel;
		}
		public CategoryModel GetCategory(Guid categoryUid)
		{
			int? categoryId = GetCategoryIdFromCategoryUid(categoryUid).Result;
			if (categoryId == null)
			{
				throw new InvalidOperationException("Category doesn't exist in database.");
			}
			CategoryModel category = _categoryRepository.GetCategoryByCategoryId(categoryId);
			category.ParentCategoryUid = new Guid();
			return category;
		}

		public async Task DeleteCategory(Guid uid)
		{
			int? categoryId = GetCategoryIdFromCategoryUid(uid).Result;
			if (categoryId == null)
			{
				throw new InvalidOperationException("Category doens't exist.");
			}
			await _categoryRepository.DeleteCategory(categoryId);
		}
		public Task UpdateCategory(Guid uid, string categoryName)
		{
			return _categoryRepository.UpdateCategory(uid, categoryName);
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

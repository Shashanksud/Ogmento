using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;

namespace OgmentoAPI.Domain.Catalog.Infrastructure.Repository
{
	public class CategoryRepository: ICategoryRepository
	{
		private readonly CatalogDbContext _dbContext;
		
		public CategoryRepository(CatalogDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<int?> GetCategoryIdFromUid(Guid Uid)
		{
			Category category = await _dbContext.Category.FirstOrDefaultAsync(x => x.CategoryUid == Uid);
			return category.CategoryID;
		}
		public List<Category> GetAllParentCategories()
		{
			return _dbContext.Category.AsNoTracking().Where(x => x.ParentCategoryId == 1).ToList();
		}
		public async Task<Guid> GetParentGuid(int? parentId) { 
			if(parentId == null)
			{
				return _dbContext.Category.AsNoTracking().FirstOrDefault(x => x.CategoryName == "All Products").CategoryUid;
			}
			return _dbContext.Category.AsNoTracking().FirstOrDefault(x => x.CategoryID == parentId).CategoryUid;
		}
		public List<CategoryModel> GetSubCategories(int categoryId)
		{

			List<Category> subCategories = _dbContext.Category.AsNoTracking().Where(x => x.ParentCategoryId == categoryId).ToList();
			List<CategoryModel> categoryModel = subCategories.Select(x => new CategoryModel
			{
				CategoryName = x.CategoryName,
				CategoryId = x.CategoryID,
				CategoryUid = x.CategoryUid,
				ParentCategoryId = x.ParentCategoryId,
				ParentCategoryUid = GetParentGuid(x.ParentCategoryId).Result,
				SubCategories = SubCategoriesCount(x.CategoryID)? GetSubCategories(x.CategoryID) : null,

			}).ToList();
			return categoryModel;

		}
		
		private bool SubCategoriesCount(int categoryId)
		{
			return _dbContext.Category.Count(x => x.ParentCategoryId == categoryId)!=0 ? true : false;
		}

		public List<CategoryModel> GetCategoriesByProductId(int productId)
		{
			//I will change this. I know this has issues.
			List<int> categoryIds = _dbContext.ProductCategoryMapping.Where(x => x.ProductId == productId).Select(x => x.CategoryId).ToList();
			List<Category> categories = _dbContext.Category.Where(x => categoryIds.Contains(x.CategoryID)).ToList();
			List<CategoryModel> categoryModels = categories.Select(x => new CategoryModel
			{
				CategoryId = x.CategoryID,
				CategoryName = x.CategoryName,
				CategoryUid = x.CategoryUid,
				ParentCategoryId = x.ParentCategoryId,
				ParentCategoryUid = GetParentGuid(x.ParentCategoryId).Result,
				SubCategories = SubCategoriesCount(x.CategoryID)? GetSubCategories(x.CategoryID) : null,
			}).ToList();
			return categoryModels;

		}
		public CategoryModel GetCategory(int? categoryId)
		{
			Category category = _dbContext.Category.FirstOrDefault(x => x.CategoryID == categoryId);
			CategoryModel categoryModel = new CategoryModel()
			{
				CategoryId = category.CategoryID,
				CategoryName = category.CategoryName,
				CategoryUid = category.CategoryUid,
				ParentCategoryId = category.ParentCategoryId,
				ParentCategoryUid = GetParentGuid(category.ParentCategoryId).Result,
				SubCategories = GetSubCategories(category.CategoryID),
			};
			return categoryModel;
		}
		private bool IsSafeDelete(int categoryId)
		{
			return !_dbContext.ProductCategoryMapping.Any(x => x.CategoryId == categoryId);
			
		}
		public async Task DeleteCategory(int? categoryId)
		{
			Category category = _dbContext.Category.FirstOrDefault(x => x.CategoryID == categoryId);
			if(category == null)
			{
				throw new InvalidOperationException($"Category with Id: {category.CategoryID} cannot be found");
			}
			if (!IsSafeDelete(category.CategoryID))
			{
				throw new InvalidOperationException($"Category with Id: {category.CategoryID} cannot be deleted as it is mapped with a product");
			}
			if (SubCategoriesCount(category.CategoryID))
			{
				List<int> subCategoryIds = _dbContext.Category.Where(x => x.ParentCategoryId == categoryId).Select(x => x.CategoryID).ToList();
				foreach (int subCategoryId in subCategoryIds)
				{
					DeleteCategory(subCategoryId);

				}
			}
			_dbContext.Category.Remove(category);
			_dbContext.SaveChanges();
		}

		public async Task UpdateCategory(Guid categoryUid, String categoryName)
		{
			int? categoryId = GetCategoryIdFromUid(categoryUid).Result;
			if (categoryId == null)
			{
				throw new InvalidOperationException("Category Cannot be found");
			}
			Category category = _dbContext.Category.FirstOrDefault(x => x.CategoryID == categoryId.Value);
			category.CategoryName = categoryName;
			_dbContext.Update(category);
			await _dbContext.SaveChangesAsync();
		}
		private async Task<CategoryModel> AddCategoryToDatabase(CategoryModel categoryModel)
		{
			Category category = new Category()
			{
				CategoryName = categoryModel.CategoryName,
				ParentCategoryId = categoryModel.ParentCategoryId,
			};
			EntityEntry<Category> entity = _dbContext.Category.Add(category);
			await _dbContext.SaveChangesAsync();
			categoryModel.CategoryId = entity.Entity.CategoryID;
			categoryModel.CategoryUid = entity.Entity.CategoryUid;
			return categoryModel;
		}
		private async Task AddSubCategories(List<CategoryModel> subCategories, int parentId)
		{
			foreach (var subCategory in subCategories)
			{
				if(subCategory.CategoryUid == Guid.Empty)
				{
					if (CategoryAlreadyExists(subCategory.CategoryName))
					{
						throw new InvalidOperationException($"{subCategory.CategoryName} Already Exists.");
					}
					subCategory.ParentCategoryId = parentId;
					CategoryModel model = await AddCategoryToDatabase(subCategory);
					subCategory.CategoryId = model.CategoryId;
					subCategory.CategoryUid = model.CategoryUid;
					subCategory.ParentCategoryUid = GetCategory(parentId).CategoryUid;
				}
				if (subCategory.SubCategories != null)
				{
					await AddSubCategories(subCategory.SubCategories, subCategory.CategoryId);
				}
			}
		}
		private bool CategoryAlreadyExists(string categoryName) {
			return _dbContext.Category.Any(x => x.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
		}
		public async Task<CategoryModel> AddCategory(CategoryModel categoryModel)
		{
			if (CategoryAlreadyExists(categoryModel.CategoryName))
			{
				throw new InvalidOperationException($"{categoryModel.CategoryName} Already Exists.");
			}
			if(categoryModel.CategoryUid == Guid.Empty)
			{
				categoryModel.ParentCategoryId = 1;
				categoryModel.ParentCategoryUid = await GetParentGuid(categoryModel.ParentCategoryId);
				categoryModel = await AddCategoryToDatabase(categoryModel);
			}
			if (categoryModel.SubCategories != null)
			{
				await AddSubCategories(categoryModel.SubCategories, categoryModel.CategoryId);
			}

			return categoryModel;
		}
		public Task<CategoryModel> AddNewCategory(CategoryModel categoryModel)
		{
			if (CategoryAlreadyExists(categoryModel.CategoryName)) {
				throw new InvalidOperationException("Category Already exists.");
			}
			if(categoryModel.ParentCategoryUid == Guid.Empty)
			{
				categoryModel.ParentCategoryId = 1;
				categoryModel.ParentCategoryUid = GetParentGuid(categoryModel.ParentCategoryId).Result;
			}
			else
			{
				categoryModel.ParentCategoryId = _dbContext.Category.First(x => x.CategoryUid == categoryModel.ParentCategoryUid).CategoryID;
			}
			return AddCategoryToDatabase(categoryModel);
		}
	}
}


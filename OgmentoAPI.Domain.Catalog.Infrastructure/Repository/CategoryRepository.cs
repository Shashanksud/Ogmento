using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Common.Abstractions.CustomExceptions;

namespace OgmentoAPI.Domain.Catalog.Infrastructure.Repository
{
	public class CategoryRepository: ICategoryRepository
	{
		private readonly CatalogDbContext _dbContext;
		
		public CategoryRepository(CatalogDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<List<CategoryModel>> GetAllCategories()
		{
			List<Category> parentCategories = await _dbContext.Category.AsNoTracking().Where(x => x.ParentCategoryId == 1).ToListAsync();
			List<CategoryModel> categoryModel =  parentCategories.Select( x => new CategoryModel
			{
				CategoryName = x.CategoryName,
				CategoryId = x.CategoryID,
				ParentCategoryId = x.ParentCategoryId,
				CategoryUid = x.CategoryUid,
				ParentCategoryUid = GetCategoryUid(x.ParentCategoryId),
				SubCategories = GetSubCategories(x.CategoryID),
			}).ToList();
			return categoryModel;
		}
		public async Task<int> GetCategoryIdAsync(Guid categoryUid)
		{
			Category category = await _dbContext.Category.FirstOrDefaultAsync(x => x.CategoryUid == categoryUid);
			if (category == null) {
				throw new EntityNotFoundException($"No Category found with Uid: {categoryUid}.");
			}
			return category.CategoryID;
		}
		public Guid GetCategoryUid(int? categoryId)
		{
			if (categoryId == null)
			{
				return _dbContext.Category.AsNoTracking().Single(x => x.CategoryName == "All Products").CategoryUid;
			}
			return  _dbContext.Category.AsNoTracking().Single(x => x.CategoryID == categoryId).CategoryUid;
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
				ParentCategoryUid = GetCategoryUid(x.ParentCategoryId),
				SubCategories = CheckSubCategoriesExists(x.CategoryID)? GetSubCategories(x.CategoryID) : null,

			}).ToList();
			return categoryModel;

		}
		
		private bool CheckSubCategoriesExists(int categoryId)
		{
			return _dbContext.Category.Any(x => x.ParentCategoryId == categoryId);
		}
		public async Task<CategoryModel> GetCategory(Guid categoryUid)
		{
			int categoryId = await GetCategoryIdAsync(categoryUid);
			Category category = _dbContext.Category.Single(x => x.CategoryID == categoryId);
			CategoryModel categoryModel = new CategoryModel()
			{
				CategoryId = category.CategoryID,
				CategoryName = category.CategoryName,
				CategoryUid = category.CategoryUid,
				ParentCategoryId = category.ParentCategoryId,
				ParentCategoryUid = new Guid(),
				SubCategories = GetSubCategories(category.CategoryID),
			};
			return categoryModel;
		}
		private bool IsSafeDelete(int categoryId)
		{
			return !_dbContext.ProductCategoryMapping.Any(x => x.CategoryId == categoryId);
			
		}
		public async Task DeleteCategory(Guid categoryUid)
		{
			int categoryId = await GetCategoryIdAsync(categoryUid);
			List<int> categoryIds = new List<int> { categoryId};
			if (!IsSafeDelete(categoryId))
			{
				throw new DatabaseOperationException($"Category cannot be deleted as it is mapped with a product");
			}
			if (CheckSubCategoriesExists(categoryId))
			{
				List<int> subCategoryIds = _dbContext.Category.Where(x => x.ParentCategoryId == categoryId).Select(x => x.CategoryID).ToList();
				categoryIds.AddRange(subCategoryIds);
				foreach (int subCategoryId in subCategoryIds)
				{
					if (CheckSubCategoriesExists(subCategoryId))
					{
						categoryIds.AddRange(_dbContext.Category.Where(x => x.ParentCategoryId == subCategoryId).Select(x => x.CategoryID));
					}
				}
			}
			int rowsDeleted = await _dbContext.Category.Where(x => categoryIds.Contains(x.CategoryID)).ExecuteDeleteAsync();
			if (rowsDeleted == 0) {
				throw new DatabaseOperationException("Unable to delete the category.");
			}
		}
		public async Task UpdateCategory(Guid categoryUid, String categoryName)
		{
			int categoryId = GetCategoryIdAsync(categoryUid).Result;
			Category category = _dbContext.Category.Single(x => x.CategoryID == categoryId);
			category.CategoryName = categoryName;
			_dbContext.Category.Update(category);
			int rowsUpdated = await _dbContext.SaveChangesAsync();
			if (rowsUpdated == 0)
			{
				throw new DatabaseOperationException("Unable to Update the category.");
			}
		}
		private async Task<CategoryModel> AddCategoryToDatabase(CategoryModel categoryModel)
		{
			Category category = new Category()
			{
				CategoryName = categoryModel.CategoryName,
				ParentCategoryId = categoryModel.ParentCategoryId,
				CategoryUid = Guid.NewGuid()
			};
			EntityEntry<Category> entity = _dbContext.Category.Add(category);
			int rowsAdded = await _dbContext.SaveChangesAsync();
			if (rowsAdded == 0)
			{
				throw new DatabaseOperationException($"Unable to Add the category with Uid: {categoryModel.CategoryUid}.");
			}
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
					subCategory.ParentCategoryUid = GetCategoryUid(parentId);
				}
				if (subCategory.SubCategories != null)
				{
					await AddSubCategories(subCategory.SubCategories, subCategory.CategoryId);
				}
			}
		}
		private bool CategoryAlreadyExists(string categoryName) {
			Category category = _dbContext.Category.FirstOrDefault(x => x.CategoryName.ToLower() == categoryName.ToLower());
            if(category == null)
            {
                return false;
            }
            return true;
		}
		public async Task<CategoryModel> AddCategory(CategoryModel categoryModel)
		{
			if (CategoryAlreadyExists(categoryModel.CategoryName))
			{
				throw new InvalidDataException($"{categoryModel.CategoryName} Already Exists.");
			}
			if(categoryModel.CategoryUid == Guid.Empty)
			{
				categoryModel.ParentCategoryId = 1;
				categoryModel.ParentCategoryUid = GetCategoryUid(categoryModel.ParentCategoryId);
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
            if (categoryModel == null || string.IsNullOrEmpty(categoryModel.CategoryName))
            {
                throw new InvalidDataException("Category name cannot be null or empty.");
            }
            if (CategoryAlreadyExists(categoryModel.CategoryName)) {
				throw new InvalidDataException($"{categoryModel.CategoryName} Already Exists.");
			}
			if(categoryModel.ParentCategoryUid == Guid.Empty)
			{
				categoryModel.ParentCategoryId = 1;
				categoryModel.ParentCategoryUid = GetCategoryUid(categoryModel.ParentCategoryId);
			}
			else
			{
				categoryModel.ParentCategoryId = _dbContext.Category.First(x => x.CategoryUid == categoryModel.ParentCategoryUid).CategoryID;
			}
			return AddCategoryToDatabase(categoryModel);
		}
	}
}


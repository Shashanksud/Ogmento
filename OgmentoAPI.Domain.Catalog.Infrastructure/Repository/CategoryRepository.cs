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
		public async Task<int?> GetCategoryIdFromCategoryUid(Guid Uid)
		{
			Category category = await _dbContext.Category.FirstOrDefaultAsync(x => x.CategoryUid == Uid);
			return category.ID;
		}
		public List<Category> GetAllParentCategories()
		{
			return _dbContext.Category.AsNoTracking().Where(x => x.ParentCategoryId == 1).ToList();
		}
		public async Task<Guid> GetParentGuidFromParentId(int? parentId) { 
			if(parentId == null)
			{
				return _dbContext.Category.AsNoTracking().FirstOrDefault(x => x.CategoryName == "All Products").CategoryUid;
			}
			return _dbContext.Category.AsNoTracking().FirstOrDefault(x => x.ID == parentId).CategoryUid;
		}
		public List<CategoryModel> GetSubCategoriesByCategoryId(int Id)
		{

			List<Category> subCategories = _dbContext.Category.AsNoTracking().Where(x => x.ParentCategoryId == Id).ToList();
			List<CategoryModel> categoryModel = subCategories.Select(x => new CategoryModel
			{
				CategoryName = x.CategoryName,
				ID = x.ID,
				CategoryUid = x.CategoryUid,
				ParentCategoryId = x.ParentCategoryId,
				ParentCategoryUid = GetParentGuidFromParentId(x.ParentCategoryId).Result,
				SubCategories = SubCategoriesCount(x.ID) != 0 ? GetSubCategoriesByCategoryId(x.ID) : null,

			}).ToList();
			return categoryModel;

		}
		
		private int SubCategoriesCount(int Id)
		{
			return _dbContext.Category.Count(x => x.ParentCategoryId == Id);
		}

		public List<CategoryModel> GetCategoriesByProductId(int productId)
		{
			//I will change this. I know this has issues.
			List<int> categoryIds = _dbContext.ProductCategoryMapping.Where(x => x.ProductId == productId).Select(x => x.CategoryId).ToList();
			List<Category> categories = _dbContext.Category.Where(x => categoryIds.Contains(x.ID)).ToList();
			List<CategoryModel> categoryModels = categories.Select(x => new CategoryModel
			{
				ID = x.ID,
				CategoryName = x.CategoryName,
				CategoryUid = x.CategoryUid,
				ParentCategoryId = x.ParentCategoryId,
				ParentCategoryUid = GetParentGuidFromParentId(x.ParentCategoryId).Result,
				SubCategories = SubCategoriesCount(x.ID) != 0 ? GetSubCategoriesByCategoryId(x.ID) : null,
			}).ToList();
			return categoryModels;

		}
		public CategoryModel GetCategoryByCategoryId(int? categoryId)
		{
			Category category = _dbContext.Category.FirstOrDefault(x => x.ID == categoryId);
			CategoryModel categoryModel = new CategoryModel()
			{
				ID = category.ID,
				CategoryName = category.CategoryName,
				CategoryUid = category.CategoryUid,
				ParentCategoryId = category.ParentCategoryId,
				ParentCategoryUid = GetParentGuidFromParentId(category.ParentCategoryId).Result,
				SubCategories = GetSubCategoriesByCategoryId(category.ID),
			};
			return categoryModel;
		}
		private bool IsSafeDelete(int categoryId)
		{
			if (_dbContext.ProductCategoryMapping.Any(x => x.CategoryId == categoryId))
			{
				return false;
			}
			return true;
		}
		public async Task DeleteCategory(int? categoryId)
		{
			Category category = _dbContext.Category.FirstOrDefaultAsync(x => x.ID == categoryId).Result;
			if(category == null)
			{
				throw new InvalidOperationException($"Category with Id: {category.ID} cannot be found");
			}
			if (!IsSafeDelete(category.ID))
			{
				throw new InvalidOperationException($"Category with Id: {category.ID} cannot be deleted as it is mapped with a product");
			}
			if (SubCategoriesCount(category.ID) != 0)
			{
				List<int> subCategoryIds = _dbContext.Category.Where(x => x.ParentCategoryId == categoryId).Select(x => x.ID).ToList();
				foreach (int subCategoryId in subCategoryIds)
				{
					DeleteCategory(subCategoryId);

				}
			}
			_dbContext.Category.Remove(category);
			_dbContext.SaveChanges();
		}

		public async Task UpdateCategory(Guid uid, String categoryName)
		{
			int? id = GetCategoryIdFromCategoryUid(uid).Result;
			if (id == null)
			{
				throw new InvalidOperationException("Category Cannot be found");
			}
			Category category = _dbContext.Category.FirstOrDefault(x => x.ID == id.Value);
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
				CategoryUid = Guid.NewGuid(),
			};
			EntityEntry<Category> entity = _dbContext.Category.Add(category);
			await _dbContext.SaveChangesAsync();
			categoryModel.ID = entity.Entity.ID;
			categoryModel.CategoryUid = entity.Entity.CategoryUid;
			if (categoryModel.ParentCategoryId == 1)
			{
				categoryModel.ParentCategoryUid = new Guid();
			}
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
					subCategory.ID = model.ID;
					subCategory.CategoryUid = model.CategoryUid;
					subCategory.ParentCategoryUid = GetCategoryByCategoryId(parentId).CategoryUid;
				}
				if (subCategory.SubCategories != null)
				{
					await AddSubCategories(subCategory.SubCategories, subCategory.ID);
				}
			}
		}
		private bool CategoryAlreadyExists(string categoryName) {
			if(_dbContext.Category.Any(x=>x.CategoryName.ToLower() == categoryName.ToLower()))
			{
				return true;
			}
			return false;
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
				categoryModel.ParentCategoryUid = await GetParentGuidFromParentId(categoryModel.ParentCategoryId);
				categoryModel = await AddCategoryToDatabase(categoryModel);
			}
			if (categoryModel.SubCategories != null)
			{
				await AddSubCategories(categoryModel.SubCategories, categoryModel.ID);
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
				categoryModel.ParentCategoryUid = GetParentGuidFromParentId(categoryModel.ParentCategoryId).Result;
			}
			else
			{
				categoryModel.ParentCategoryId = _dbContext.Category.FirstOrDefault(x => x.CategoryUid == categoryModel.ParentCategoryUid).ID;
			}
			return AddCategoryToDatabase(categoryModel);
		}
	}
}


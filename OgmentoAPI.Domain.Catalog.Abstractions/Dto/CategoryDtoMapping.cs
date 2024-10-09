using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public static class CategoryDtoMapping
	{
		
		public static CategoryDto ToDto(this CategoryModel category)
		{
			CategoryDto categoryDto = new CategoryDto()
			{
				CategoryUid = category.CategoryUid,
				CategoryName = category.CategoryName,
				ParentCategoryUid = category.ParentCategoryUid,
				SubCategories = category.SubCategories != null
									? category.SubCategories.Select(ToDto).ToList()
									: [],
			};
			return categoryDto;

		}
		public static CategoryModel ToModel(this CategoryDto category)
		{
			CategoryModel categoryModel = new CategoryModel()
			{
				ParentCategoryUid = category.ParentCategoryUid,
				CategoryName = category.CategoryName,
				CategoryUid = category.CategoryUid,
				SubCategories = category.SubCategories.Select(ToModel).ToList()
			};
			return categoryModel;
		}
	}
}

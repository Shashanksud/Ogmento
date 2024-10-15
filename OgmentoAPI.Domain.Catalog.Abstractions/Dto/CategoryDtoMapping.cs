using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public static class CategoryDtoMapping
	{
		
		public static CategoryDto ToDto(this CategoryModel category)
		{
			return new CategoryDto()
			{
				CategoryUid = category.CategoryUid,
				CategoryName = category.CategoryName,
				ParentCategoryUid = category.ParentCategoryId!=1? category.ParentCategoryUid : new Guid(),
				SubCategories = category.SubCategories?.Select(ToDto).ToList() ?? [],
			};
		}
		public static CategoryModel ToModel(this CategoryDto category)
		{
			return new CategoryModel()
			{
				ParentCategoryUid = category.ParentCategoryUid,
				CategoryName = category.CategoryName,
				CategoryUid = category.CategoryUid,
				SubCategories = category.SubCategories?.Select(ToModel).ToList() ?? [],
			};
		}
		public static List<CategoryDto> ToDto(this List<CategoryModel> categories)
		{
			return categories.Select(x=>x.ToDto()).ToList();
		}
		public static List<CategoryModel> ToModel(this List<CategoryDto> categories)
		{
			return categories.Select(x=>x.ToModel()).ToList();
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;


namespace OgmentoAPI.Domain.Catalog.Api
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController: ControllerBase
	{
		private readonly ICategoryServices _categoryServices;
		public CategoryController(ICategoryServices categoryServices)
		{
			_categoryServices = categoryServices;
		}

		[HttpGet]
		[Route("GetAllCategories")]
		public IActionResult GetAllCategories()
		{

			return Ok(_categoryServices.GetAllCategories().Select(x=>x.ToDto()).ToList());
		}
		[HttpGet]
		[Route("GetCategory/{uid}")]
		public IActionResult GetCategory(Guid uid)
		{
			return Ok(_categoryServices.GetCategory(uid).ToDto());
		}
		[HttpPut]
		[Route("UpdateCategory")]
		public IActionResult UpdateCategory(Guid uid, string categoryName)
		{
			_categoryServices.UpdateCategory(uid, categoryName);
			return Ok("Updated Successfully");
		}
		[HttpDelete]
		[Route("DeleteCategory/{uid}")]
		public IActionResult DeleteCategory(Guid uid)
		{
			_categoryServices.DeleteCategory(uid);
			return Ok("DeletedSuccessfully");
		}
		[HttpPost]
		[Route("AddNewCategories")]
		public IActionResult AddCategories(List<CategoryDto> categories)
		{
			return Ok(_categoryServices.AddCategories
				(categories.Select(category => category.ToModel()).ToList())
				.Result.Select(category => category.ToDto()).ToList());
		}
		[HttpPost]
		[Route("AddNewCategory")]
		public IActionResult AddNewCategory(CategoryDto categoryDto)
		{
			return Ok(_categoryServices.AddNewCategory(categoryDto.ToModel()).Result.ToDto());
		}
	}
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;


namespace OgmentoAPI.Domain.Catalog.Api
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]

	public class CategoryController: ControllerBase
	{
		private readonly ICategoryServices _categoryServices;
		public CategoryController(ICategoryServices categoryServices)
		{
			_categoryServices = categoryServices;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{

			return Ok((await _categoryServices.GetAllCategories()).ToDto());
		}
		[HttpGet]
		[Route("{categoryUid}")]
		public async Task<IActionResult> GetCategory(Guid categoryUid)
		{
			return Ok((await _categoryServices.GetCategory(categoryUid)).ToDto());
		}
		[HttpPut]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto request)
		{
			await _categoryServices.UpdateCategory(request.CategoryUid, request.CategoryName);
			return Ok();
		}
		[HttpDelete]
		[Route("{categoryUid}")]
		public async Task<IActionResult> DeleteCategory(Guid categoryUid)
		{
			await _categoryServices.DeleteCategory(categoryUid);
			return Ok();
		}
		[HttpPost]
		[Route("upload")]
		public async Task<IActionResult> AddCategories(List<CategoryDto> categories)
		{
			return Ok((await _categoryServices.AddCategories
				(categories.ToModel())).ToDto());
		}
		[HttpPost]
		public async Task<IActionResult> AddNewCategory(CategoryDto categoryDto)
		{
			return Ok((await _categoryServices.AddNewCategory(categoryDto.ToModel())).ToDto());
		}
	}
}

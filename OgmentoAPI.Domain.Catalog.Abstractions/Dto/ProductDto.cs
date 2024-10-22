using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Dto;


namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public class ProductDto: ProductBase
	{
		
		public List<PictureDto> Images { get; set; }
		public CategoryDto Category { get; set; }
	}
}

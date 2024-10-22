using Mapster;
using OgmentoAPI.Domain.Client.Abstractions.Models;

namespace OgmentoAPI.Domain.Client.Abstractions.Dto
{
	
	 public static class SalesCentersDtoMapping
	 {
		 public static SalesCentersDto ToDto(this SalesCenterModel salesCenter)
		 {
			 SalesCentersDto dto = new SalesCentersDto()
			 {

				 SalesCenterName = salesCenter.SalesCenterName,
				 City = salesCenter.City,
				 CountryId = salesCenter.CountryId,

			 };
			 return dto;
		 }
		 public static List<SalesCentersDto> ToDto(this List<SalesCenterModel> salesCenterList)
		 {
			 return salesCenterList.Select(salesCenter => new SalesCentersDto()
			 {
				 SalesCenterUid = salesCenter.SalesCenterUid,
				 SalesCenterName = salesCenter.SalesCenterName,
				 City = salesCenter.City,
				 CountryId = salesCenter.CountryId,

			 }).ToList();

		 }
		 public static SalesCenterModel ToModel(this SalesCentersDto salesCentersDtos)
		 {

			 return new SalesCenterModel()
			 {
				 SalesCenterUid = salesCentersDtos.SalesCenterUid,
				 SalesCenterName = salesCentersDtos.SalesCenterName,
				 City = salesCentersDtos.City,
				 CountryId = salesCentersDtos.CountryId,
			 };
		 }
	 }
}



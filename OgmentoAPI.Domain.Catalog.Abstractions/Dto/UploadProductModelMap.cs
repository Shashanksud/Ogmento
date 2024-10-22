using CsvHelper.Configuration;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;


namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public class UploadProductModelMap : ClassMap<UploadProductModel>
	{
		public UploadProductModelMap()
		{
			Map(m => m.SkuCode).Name("SkuCode");
			Map(m => m.ProductName).Name("ProductName");
			Map(m => m.ProductDescription).Name("ProductDescription");
			Map(m => m.Price).Name("Price");
			Map(m => m.LoyaltyPoints).Name("LoyaltyPoints");
			Map(m => m.Weight).Name("Weight");
			Map(m => m.ExpiryDate).Name("ExpiryDate").TypeConverterOption.Format("yyyy-MM-dd");
			Map(m => m.CategoryIds).Name("CategoryIds").TypeConverter<TypeConverter>();
		}
	}
}



using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace OgmentoAPI.Domain.Catalog.Abstractions.Dto
{
	public class TypeConverter: DefaultTypeConverter
	{
		public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
		{
			return text?.Split(',').Select(int.Parse).ToList() ?? [];
		}
	}
}

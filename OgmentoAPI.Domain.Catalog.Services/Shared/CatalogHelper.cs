using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using System.Globalization;

namespace OgmentoAPI.Domain.Catalog.Services.Shared
{
	public class CatalogHelper
	{
		public static async Task UploadCsvFile<SourceModel, TargetModel>(IFormFile csvFile, Func<List<SourceModel>, Task> uploadFunction) where TargetModel : ClassMap<SourceModel>
		{
			CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = typeof(SourceModel) == typeof(UploadPictureModel) ? "," : ";"
			};

			using (StreamReader csvStreamReader = new StreamReader(csvFile.OpenReadStream()))
			using (CsvReader csvReader = new CsvReader(csvStreamReader, csvConfig))
			{
				csvReader.Context.RegisterClassMap<TargetModel>();
				List<SourceModel> records = csvReader.GetRecords<SourceModel>().ToList();
				await uploadFunction(records);
			}
		}
	}
}

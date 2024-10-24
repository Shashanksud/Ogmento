namespace OgmentoAPI.Domain.Client.Abstractions.Dto
{
	public class KioskDto
	{
		public string KioskName { get; set; }
		public Tuple<Guid, string> SalesCenter { get; set; }
	}
}

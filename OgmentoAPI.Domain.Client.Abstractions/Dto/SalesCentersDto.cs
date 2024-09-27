namespace OgmentoAPI.Domain.Client.Abstractions.Dto
{
    public class SalesCentersDto 
    {

        public Guid SalesCenterUid { get; set; }
        public string SalesCenterName { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
    }
}

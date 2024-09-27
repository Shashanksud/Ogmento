
namespace OgmentoAPI.Domain.Client.Abstractions.Models
{
    public class SalesCenterModel
    {
        public Guid SalesCenterUid { get; set; }
        public int SalesCenterId { get; set; }
        public string SalesCenterName { get; set; }

        public int CountryId{ get; set; }
        public string City { get; set; }
    }
}

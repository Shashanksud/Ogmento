using OgmentoAPI.Domain.Common.Abstractions.DataContext;
using System.ComponentModel.DataAnnotations;

namespace OgmentoAPI.Domain.Client.Abstractions.DataContext
{
    public class SalesCenter
    {
        [Key]
        public int ID { get; set; }
        public Guid SalesCenterUid { get; set; }
        public string SalesCenterName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    }
}

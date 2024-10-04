using OgmentoAPI.Domain.Common.Abstractions.DataContext;

namespace OgmentoAPI.Domain.Catalog.Abstractions.DataContext
{
    public class ProductImageMapping
    {
		public int ProductId { get; set; }
		public int ImageId { get; set; }
    }
}

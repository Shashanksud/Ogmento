

namespace OgmentoAPI.Domain.Catalog.Abstractions.DataContext
{
    public class ProductCategoryMapping
    {
		public int ProductId {  get; set; }
		public int CategoryId { get; set; }
		public Product Product { get; set; }
		public Category Category { get; set; }
    }
}

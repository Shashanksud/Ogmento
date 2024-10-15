
using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Common.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Services;

namespace OgmentoAPI.Domain.Catalog.Infrastructure.Repository
{
	public class ProductRepository: IProductRepository
	{
	
		private readonly CatalogDbContext _dbContext;
		private readonly IPictureService _pictureService;
		private readonly ICategoryRepository _categoryRepository;
		public ProductRepository(ICategoryRepository categoryRepository, CatalogDbContext dbContext, IPictureService pictureService)
		{
			_categoryRepository = categoryRepository;
			_dbContext = dbContext;
			_pictureService = pictureService;
		}
		public List<PictureModel> GetImages(int productId)
		{
			List<int> pictureIds = _dbContext.ProductImageMapping.Where(x => x.ProductId == productId).Select(x => x.ImageId).ToList();
			List<PictureModel> pictureModels = _pictureService.GetImagesByPictureId(pictureIds);
			return pictureModels;
		}

		public List<ProductModel> GetAllProducts()
		{
			List<Product> products = _dbContext.Product.ToList();
			List<ProductModel> productModel = products.Select(x => new ProductModel
			{
				ProductId = x.ProductID,
				ProductName = x.ProductName,
				SkuCode = x.SkuCode,
				LoyaltyPoints = x.LoyaltyPoints,
				Price = x.Price,
				ExpiryDate = x.ExpiryDate,
				ProductDescription = x.ProductDescription,
				Images = GetImages(x.ProductID),
				Categories = _categoryRepository.GetCategoriesByProductId(x.ProductID)

			}).ToList();
			return productModel;
		}
	}
}

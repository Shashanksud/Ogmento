using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;
using OgmentoAPI.Domain.Catalog.Abstractions.Dto;
using OgmentoAPI.Domain.Catalog.Abstractions.Models;
using OgmentoAPI.Domain.Catalog.Abstractions.Repository;
using OgmentoAPI.Domain.Catalog.Abstractions.Services;
using OgmentoAPI.Domain.Common.Abstractions.CustomExceptions;
using OgmentoAPI.Domain.Common.Abstractions.Models;
using OgmentoAPI.Domain.Common.Abstractions.Services;

namespace OgmentoAPI.Domain.Catalog.Infrastructure.Repository
{
	public class ProductRepository: IProductRepository
	{
	
		private readonly CatalogDbContext _dbContext;
		private readonly IPictureService _pictureService;
		private readonly ICategoryServices _categoryServices;
		public ProductRepository(ICategoryServices categoryServices, CatalogDbContext dbContext, IPictureService pictureService)
		{
			_categoryServices = categoryServices;
			_dbContext = dbContext;
			_pictureService = pictureService;
		}
		public async Task<List<PictureModel>> GetImages(int productId)
		{
			List<int> pictureIds = _dbContext.ProductImageMapping.Where(x => x.ProductId == productId).Select(x => x.ImageId).ToList();
			List<PictureModel> pictureModels = await _pictureService.GetPictures(pictureIds);
			return pictureModels;
		}
		private async Task<CategoryModel> GetCategory(int productId) {
			List<int> productCategoryIds = _dbContext.ProductCategoryMapping.Where(x => x.ProductId == productId ).Select(x=>x.CategoryId).ToList();
			List<Guid> productCategoryUids = productCategoryIds.Select(x => _categoryServices.GetCategoryUid(x)).ToList();
			List<CategoryModel> productCategories = new List<CategoryModel>();
			foreach (Guid productCategoryUid in productCategoryUids)
			{
				productCategories.Add(await _categoryServices.GetCategoryForProduct(productCategoryUid));
			}
			CategoryModel category = productCategories.Single(x => x.ParentCategoryId == 1);
			category.ParentCategoryUid = new Guid();
			category.SubCategories = productCategories.Where(x=>x.ParentCategoryId==category.CategoryId).ToList();
			foreach(CategoryModel subCategory in category.SubCategories)
			{
				subCategory.SubCategories= productCategories.Where(x=>x.ParentCategoryId== subCategory.CategoryId).ToList();
			}
			return category;
		}

		public async Task<List<ProductModel>> GetAllProducts()
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
				Images = GetImages(x.ProductID).GetAwaiter().GetResult(),
				Category = GetCategory(x.ProductID).GetAwaiter().GetResult(),
			}).ToList();
			return productModel;
		}

		public async Task<ProductModel> GetProduct(string sku)
		{
			Product product = _dbContext.Product.FirstOrDefault(x => x.SkuCode == sku);
			if (product == null) {
				throw new EntityNotFoundException($"Product {sku} not found.");
			}
			ProductModel productModel = new ProductModel()
			{
				ProductId= product.ProductID,
				ProductName= product.ProductName,
				SkuCode= sku,
				Images = GetImages(product.ProductID).GetAwaiter().GetResult(),
				Category = GetCategory(product.ProductID).GetAwaiter().GetResult(),
				Price = product.Price,
				ExpiryDate = product.ExpiryDate,
				ProductDescription = product.ProductDescription,
				LoyaltyPoints= product.LoyaltyPoints,
			};
			return productModel;
		}
		private async Task AddProductCategoryMapping(List<int> CategoryIds, int productId)
		{
			List<ProductCategoryMapping> productCategories = CategoryIds.Select(x => new ProductCategoryMapping
			{
				ProductId = productId,
				CategoryId = x
			}).ToList();
			await _dbContext.ProductCategoryMapping.AddRangeAsync(productCategories);
			int rowsAdded = await _dbContext.SaveChangesAsync();
			if (rowsAdded == 0)
			{
				throw new DatabaseOperationException($"Unable to Add the Product Category mapping.");
			}

		} 
		private async Task UpdateProductImageMapping(List<PictureModel> pictures, int productId)
		{
			foreach (PictureModel picture in pictures) {
				if (picture.ToBeDeleted)
				{
					ProductImageMapping productImage = _dbContext.ProductImageMapping.First(x => x.ImageId == picture.PictureId);
					_dbContext.ProductImageMapping.Remove(productImage);
					int rowsDeleted = await _dbContext.SaveChangesAsync();
					if (rowsDeleted == 0)
					{
						throw new DatabaseOperationException($"Unable to Delete the image.");
					}
					await _pictureService.DeletePicture(picture.Hash);
				}
				if (picture.IsNew)
				{
					PictureModel pictureModel = await _pictureService.AddPicture(picture);
					ProductImageMapping productImageMapping = new ProductImageMapping()
					{
						ProductId = productId,
						ImageId = pictureModel.PictureId
					};
					_dbContext.ProductImageMapping.Add(productImageMapping);
					int rowsAdded = await _dbContext.SaveChangesAsync();
					if (rowsAdded == 0)
					{
						throw new DatabaseOperationException($"Unable to Add the Product Image mapping.");
					}
				}
			}
		}
		public async Task UpdateProduct(AddProductModel productModel)
		{
			Product product = _dbContext.Product.FirstOrDefault(x => x.SkuCode == productModel.SkuCode);
			if(product == null)
			{
				throw new EntityNotFoundException($"Product {productModel.SkuCode} not found.");
			}
			product.ProductName = productModel.ProductName;
			product.ProductDescription = productModel.ProductDescription;
			product.Price = productModel.Price;
			product.ExpiryDate = productModel.ProductExpiry;
			product.LoyaltyPoints = productModel.LoyaltyPoints;
			_dbContext.Product.Update(product);
			int productRowsUpdated = await _dbContext.SaveChangesAsync();
			if(productRowsUpdated == 0)
			{
				throw new DatabaseOperationException($"Unable to Update Product {productModel.SkuCode}.");
			}
			await _dbContext.ProductCategoryMapping.Where(x => x.ProductId == product.ProductID).ExecuteDeleteAsync();
			List<int> categoryIds = new List<int>();
			foreach (Guid categoryGuid in productModel.Categories)
			{
				categoryIds.Add(await _categoryServices.GetCategoryId(categoryGuid));
			}
			await AddProductCategoryMapping(categoryIds,product.ProductID);
			await UpdateProductImageMapping(productModel.Images, product.ProductID);
		}

		public async Task DeleteProduct(string sku)
		{
			Product product = _dbContext.Product.FirstOrDefault(x => x.SkuCode == sku);
			if(product == null)
			{
				throw new EntityNotFoundException($"Product {sku} cannot be found.");
			}
			int rowsDeleted = await _dbContext.ProductCategoryMapping.Where(x => x.ProductId == product.ProductID).ExecuteDeleteAsync();
			if (rowsDeleted == 0)
			{
				throw new DatabaseOperationException($"Unable to Delete Product {sku} Category Mappings.");
			}
			List<int> pictureIds = _dbContext.ProductImageMapping.Where(x => x.ProductId == product.ProductID).Select(x=>x.ImageId).ToList();
			if (pictureIds.Count != 0)
			{
				await _dbContext.ProductImageMapping.Where(x => x.ProductId == product.ProductID).ExecuteDeleteAsync();
			}
			_dbContext.Product.Remove(product);
			rowsDeleted = await _dbContext.SaveChangesAsync();
			if (rowsDeleted == 0)
			{
				throw new DatabaseOperationException($"Unable to Delete Product {sku}.");
			}
			if (pictureIds.Count != 0)
			{
				await _pictureService.DeletePictures(pictureIds);
			}
		}

		public async Task AddProduct(AddProductModel productModel)
		{
			bool skuExists = _dbContext.Product.Any(x => x.SkuCode == productModel.SkuCode);
			if (skuExists)
			{
				throw new InvalidDataException($"Product with skucode: {productModel.SkuCode} already exists. Please give different code.");
			}
			Product product = new Product()
			{
				SkuCode = productModel.SkuCode,
				Price = productModel.Price,
				ProductDescription = productModel.ProductDescription,
				ProductName = productModel.ProductName,
				LoyaltyPoints = productModel.LoyaltyPoints,
				ExpiryDate = productModel.ProductExpiry,
				Weight = productModel.Weight,
			};
			EntityEntry<Product> productEntry = _dbContext.Product.Add(product);
			int rowsAdded = await _dbContext.SaveChangesAsync();
			if(rowsAdded == 0)
			{
				throw new DatabaseOperationException($"Product {productModel.SkuCode} not Added.");
			}
			productModel.ProductId = productEntry.Entity.ProductID;
			if (productModel.Images.Count != 0)
			{
				await UpdateProductImageMapping(productModel.Images, productModel.ProductId);
			}
			if (productModel.Categories.Count!=0)
			{
				List<int> categoryIds = new List<int>();
				foreach(Guid categoryGuid in productModel.Categories)
				{
					categoryIds.Add(await _categoryServices.GetCategoryId(categoryGuid));
				}
				await AddProductCategoryMapping(categoryIds, productModel.ProductId);
			}
			
		}
		public async Task UploadProducts(List<UploadProductModel> products)
		{
			List<AddProductModel> productModels = products.Select(x => new AddProductModel {
				ProductName = x.ProductName,
				ProductDescription = x.ProductDescription,
				Price = x.Price,
				Weight = x.Weight,
				ProductExpiry = x.ExpiryDate,
				LoyaltyPoints = x.LoyaltyPoints,
				SkuCode = x.SkuCode,
			}).ToList();
			foreach (AddProductModel productModel in productModels) {
				await AddProduct(productModel);
			}
			foreach (UploadProductModel product in products)
			{
				int productId = _dbContext.Product.Single(x => x.SkuCode == product.SkuCode).ProductID;
				await AddProductCategoryMapping(product.CategoryIds, productId);
			}
		}

		public async Task UploadPictures(List<UploadPictureModel> pictures)
		{
			foreach (UploadPictureModel picture in pictures) {
				PictureModel pictureModel = await _pictureService.AddPicture(new PictureModel
				{
					FileName = picture.FileName,
					MimeType = picture.MimeType,
					BinaryData = Convert.FromBase64String(picture.Base64EncodedData)
				});
				int? productId = _dbContext.Product.FirstOrDefault(x => x.SkuCode == picture.SkuCode).ProductID;
				if (productId == null) {
					throw new EntityNotFoundException($"Product {picture.SkuCode} doesn't exist");
				}
				_dbContext.ProductImageMapping.Add(new ProductImageMapping
				{
					ProductId = productId.Value,
					ImageId = pictureModel.PictureId,
				});
				int rowsAdded = await _dbContext.SaveChangesAsync();
				if (rowsAdded == 0)
				{
					throw new DatabaseOperationException($"Unable to add {pictureModel.FileName}.");
				}
			}
			
		}
	}
}

using Microsoft.EntityFrameworkCore;
using OgmentoAPI.Domain.Catalog.Abstractions.DataContext;

namespace OgmentoAPI.Domain.Catalog.Infrastructure
{
    public class CatalogDbContext: DbContext
    {
		public CatalogDbContext(DbContextOptions<CatalogDbContext> options):base(options)
		{
		}
		public DbSet<Product> Product { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<ProductCategoryMapping> ProductCategoryMapping { get; set; }
		public DbSet<ProductImageMapping> ProductImageMapping { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.ProductID);
				entity.Property(e => e.ProductID)
						.HasColumnName("ID").UseIdentityColumn();
				entity.HasMany(e => e.ProductCategories)
					.WithOne(e => e.Product)
					.HasForeignKey(e => e.ProductId);
			});
			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(e => e.CategoryID);
				entity.Property(e => e.CategoryID)
						.HasColumnName("ID").UseIdentityColumn();

				entity.HasOne(e => e.ParentCategory)
					.WithMany(e => e.SubCategories)
					.HasForeignKey(e => e.ParentCategoryId)
					.OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(e => e.SubCategories)
					.WithOne(e => e.ParentCategory)
					.HasForeignKey(e => e.ParentCategoryId);

				entity.HasMany(e => e.ProductCategories)
					.WithOne(e => e.Category)
					.HasForeignKey(e => e.CategoryId);
			});

			modelBuilder.Entity<ProductCategoryMapping>(entity =>
			{
				entity.HasKey(e => new { e.ProductId, e.CategoryId });

				entity.HasOne(e => e.Product)
					.WithMany(e => e.ProductCategories)
					.HasForeignKey(e => e.ProductId);

				entity.HasOne(e => e.Category)
					.WithMany(e => e.ProductCategories)
					.HasForeignKey(e => e.CategoryId);
			});
			modelBuilder.Entity<ProductImageMapping>(entity =>
			{
				entity.HasKey(e => new {e.ProductId, e.ImageId });
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}

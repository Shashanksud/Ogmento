using Microsoft.EntityFrameworkCore;
using OgmentoAPI.Domain.Common.Abstractions.DataContext;

namespace OgmentoAPI.Domain.Common.Infrastructure
{
	public class CommonDBContext : DbContext
	{
		public CommonDBContext(DbContextOptions<CommonDBContext> options) : base(options) { }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Picture> Pictures { get; set; }
		public DbSet<PictureBinary> PictureBinary { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Picture>(entity =>
			{
				entity.HasKey(e => e.PictureID);
				entity.Property(e => e.PictureID)
						.HasColumnName("Id");
			});
			modelBuilder.Entity<PictureBinary>(entity =>
			{
				entity.HasKey(e => e.PictureBinaryID);
				entity.Property(e => e.PictureBinaryID)
						.HasColumnName("Id");
			});
			base.OnModelCreating(modelBuilder);
		}
	}
}

using Microsoft.EntityFrameworkCore;
using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Domain.Authorization.Abstraction.DataContext;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;
using OgmentoAPI.Domain.Client.Abstractions.DataContext;



namespace OgmentoAPI.Web.DataContext
{
    public partial class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext()
        {
        }

        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<RolesMaster> RolesMaster { get; set; }
        public virtual DbSet<UsersMaster> UsersMaster { get; set; }

    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.JwtId).IsRequired();

                entity.Property(e => e.Token).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_UsersMaster");
            });

            modelBuilder.Entity<RolesMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);
            });

            

            modelBuilder.Entity<UsersMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.UserName).IsRequired();
            });
            modelBuilder.Entity<UsersMaster>()
                .HasOne(x => x.UserRole)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            OnModelCreatingPartial(modelBuilder);

            
            //modelBuilder.Entity<SalesCenterUserMapping>()
              //.HasKey(sc => new { sc.UserId, sc.SalesCenterId });
            // modelBuilder.Entity<SalesCenterUserMapping>().Has();

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

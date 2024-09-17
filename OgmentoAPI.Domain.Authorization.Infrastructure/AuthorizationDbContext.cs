using Microsoft.EntityFrameworkCore;
using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Domain.Authorization.Abstraction.DataContext;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;
using OgmentoAPI.Domain.Client.Abstractions.DataContext;



namespace OgmentoAPI.Web.DataContext
{
    public partial class AuthorizationDbContext : DbContext, IAuthorizationContext, IUserContext
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
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<UsersMaster> UsersMaster { get; set; }

        public List<string> GetRoleNames(long UserId)
        {
            return (from UM in UsersMaster
                    join UR in UserRoles on UM.UserId equals UR.UserId
                    join RM in RolesMaster on UR.RoleId equals RM.RoleId
                    where UM.UserId == UserId
                    select RM.RoleName).ToList();
        }



        public UserModel GetUserByID(long UserId)
        {
            return (from UM in UsersMaster
                    where UM.UserId == UserId
                    select new UserModel
                    {
                        UserId = UM.UserId,
                        FirstName = UM.FirstName,
                        LastName = UM.LastName,
                        Email = UM.Email,
                        PhoneNumber = UM.PhoneNumber,
                        UserName = UM.UserName
                    }).FirstOrDefault(); 
        }

        public UsersMaster GetUserDetail(LoginModel login)
        {
           return UsersMaster.FirstOrDefault(c => c.UserName == login.UserName && c.Password == login.Password);
        }

        public List<RolesMaster> GetUserRoles(long userID)
        {
            return (from UM in UsersMaster
             join UR in UserRoles on UM.UserId equals UR.UserId
             join RM in RolesMaster on UR.RoleId equals RM.RoleId
             where UM.UserId == userID
                    select RM).ToList();
        }

        

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

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_UserRoles_1");

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UsersMaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhoneNumber).IsRequired();

                entity.Property(e => e.UserName).IsRequired();
            });
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<UsersMaster>()
                .HasMany(x=> x.SalesCenterUsers)
                .WithOne();
            modelBuilder.Entity<SalesCenterUserMapping>()
              .HasKey(sc => new { sc.UserId, sc.SalesCenterId });
            // modelBuilder.Entity<SalesCenterUserMapping>().Has();

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

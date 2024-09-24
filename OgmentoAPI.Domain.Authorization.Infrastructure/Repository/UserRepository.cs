using OgmentoAPI.Domain.Authorization.Abstractions.DataContext;
using OgmentoAPI.Domain.Authorization.Abstractions.Enums;
using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Repository;
using OgmentoAPI.Web.DataContext;

namespace OgmentoAPI.Domain.Authorization.Infrastructure.Repository
{
    public class UserRepository : IAuthorizationRepository
    {
        private readonly AuthorizationDbContext _context;
        public UserRepository(AuthorizationDbContext context)
        {
            _context = context;
        }
        public RolesMaster GetUserRole(int userId)
        {
            UserRoles roleID = _context.UsersMaster.Find(userId).RoleId;
            RolesMaster roleMaster = _context.RolesMaster.FirstOrDefault(x => x.RoleId == roleID);
            return roleMaster;
        }
        public string GetRoleName(int userId)
        {
            var roleMaster = GetUserRole(userId);
            return roleMaster.RoleName;
        }
        public UserModel GetUserByID(int userId)
        {
            return (from UM in _context.UsersMaster
                    where UM.UserId == userId
                    select new UserModel
                    {
                        UserId = UM.UserId,
                        UserUid= UM.UserUid,
                        UserName = UM.UserName,
                        Email = UM.Email,
                        PhoneNumber = UM.PhoneNumber,
                        ValidityDays= UM.ValidityDays,
                    }).FirstOrDefault();
        }
        public UsersMaster GetUserDetail(LoginModel login)
        {
            return _context.UsersMaster.FirstOrDefault(c => c.Email== login.Email && c.Password == login.Password);
        }
        
    }
}

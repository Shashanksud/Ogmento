using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Domain.Authorization.Abstraction.DataContext;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;
using OgmentoAPI.Web.DataContext;

namespace OgmentoAPI.Domain.Authorization.Infrastructure.Repository
{
    public class UserRepository : IAuthorizationContext, IUserContext
    {
        private readonly AuthorizationDbContext _context;

        public UserRepository(AuthorizationDbContext context)
        {
            _context = context;

        }
        public List<string> GetRoleNames(int UserId)
        {
            return (from UM in _context.UsersMaster
                    join UR in _context.UserRoles on UM.UserId equals UR.UserId
                    join RM in _context.RolesMaster on UR.RoleId equals RM.RoleId
                    where UM.UserId == UserId
                    select RM.RoleName).ToList();
        }



        public UserModel GetUserByID(int UserId)
        {
            return (from UM in _context.UsersMaster
                    where UM.UserId == UserId
                    select new UserModel
                    {
                        UserId = UM.UserId,
                        Email = UM.Email,
                        PhoneNumber = UM.PhoneNumber,
                        UserName = UM.UserName
                    }).FirstOrDefault();
        }



        public UsersMaster GetUserDetail(LoginModel login)
        {
            return _context.UsersMaster.FirstOrDefault(c => c.UserName == login.UserName && c.Password == login.Password);
        }

        public List<RolesMaster> GetUserRoles(int userID)
        {
            return (from UM in _context.UsersMaster
                    join UR in _context.UserRoles on UM.UserId equals UR.UserId
                    join RM in _context.RolesMaster on UR.RoleId equals RM.RoleId
                    where UM.UserId == userID
                    select RM).ToList();
        }


    }
}

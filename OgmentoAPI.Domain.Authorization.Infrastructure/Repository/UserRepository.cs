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

        public string GetRoleName(int userId)
        {
            var roleMaster = GetUserRole(userId);
            return roleMaster.RoleName;
        }

        //commenting this function as this is not needed as we dont have multiples roles for a user
        /*public string GetRoleNames(int UserId)
        {
            return (from UM in _context.UsersMaster
                    join UR in _context.UserRoles on UM.UserId equals UR.UserId
                    join RM in _context.RolesMaster on UR.RoleId equals RM.RoleId
                    where UM.UserId == UserId
                    select RM.RoleName).ToList();
        }*/



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
            return _context.UsersMaster.FirstOrDefault(c => c.UserName == login.UserName && c.Password == login.Password);
        }

        public RolesMaster GetUserRole(int userId)
        {
            var roleID = _context.UsersMaster.Find(userId).RoleId;
                
            return _context.RolesMaster.FirstOrDefault(x => x.RoleId == roleID);
        }
        

    }
}

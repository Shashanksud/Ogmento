using System;
using System.Collections.Generic;
using System.Linq;
using TokenDemo.Web.DataContext;
using TokenDemo.Web.Models;

namespace TokenDemo.Web.Services
{
    public interface IUserService
    {
        ResponseModel<UserModel> Get(long UserId);
    }
    public class UserService : IUserService
    {
        private readonly UserAuthorization _context;

        public UserService(UserAuthorization context)
        {
            _context = context;
        }
        public ResponseModel<UserModel> Get(long UserId)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                UserModel user = (from UM in _context.UsersMaster
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
                List<string> roleNames = (from UM in _context.UsersMaster
                                          join UR in _context.UserRoles on UM.UserId equals UR.UserId
                                          join RM in _context.RolesMaster on UR.RoleId equals RM.RoleId
                                          where UM.UserId == UserId
                                          select RM.RoleName).ToList();
                if (user != null)
                {
                    user.UserRoles = roleNames;
                    response.Data = user;
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "User Not Found!";
                    return response;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

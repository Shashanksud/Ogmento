using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;




namespace OgmentoAPI.Domain.Authorization.Services
{
    
    public class UserService : IUserService
    {
        private readonly IUserContext _context;

        public UserService(IUserContext context)
        {
            _context = context;
        }
        public ResponseModel<UserModel> Get(long UserId)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                UserModel user = _context.GetUserByID(UserId);
                List<string> roleNames = _context.GetRoleNames(UserId);
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

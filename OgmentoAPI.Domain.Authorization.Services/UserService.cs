using OgmentoAPI.Domain.Authorization.Abstraction;
using OgmentoAPI.Domain.Authorization.Abstraction.Models;
using OgmentoAPI.Domain.Client.Abstractions.Service;




namespace OgmentoAPI.Domain.Authorization.Services
{
    
    public class UserService : IUserService
    {
        private readonly IUserContext _context;
        private readonly ISalesCenterService _SalesCenterService;
        public UserService(IUserContext context, ISalesCenterService salesCenterService)
        {
            _context = context;
            _SalesCenterService = salesCenterService;
        }
        public ResponseModel<UserModel> Get(int userId)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                UserModel user = _context.GetUserByID(userId);
                var salesCenterNames = _SalesCenterService.GetSalesCenterDetails(userId).Select(x=> x.SalesCenterName).ToList();
                var role= _context.GetRoleName(userId);
                if (user != null)
                {
                    user.UserSalesCenter = salesCenterNames;
                    user.UserRole= role;
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

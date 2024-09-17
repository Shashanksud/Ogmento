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
        public ResponseModel<UserModel> Get(int UserId)
        {
            ResponseModel<UserModel> response = new ResponseModel<UserModel>();

            try
            {
                UserModel user = _context.GetUserByID(UserId);
                var SalesCenterNames = _SalesCenterService.GetSalesCenterDetails(UserId).Select(x=> x.SalesCenterName).ToList();
                var Role= _context.GetRoleName(UserId);
                if (user != null)
                {
                    user.UserSalesCenter = SalesCenterNames;
                    user.UserRole= Role;
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

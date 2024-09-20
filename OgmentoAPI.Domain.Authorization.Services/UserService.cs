using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Repository;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;
using OgmentoAPI.Domain.Client.Abstractions.Service;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthorizationRepository _context;
        private readonly ISalesCenterService _SalesCenterService;
        public UserService(IAuthorizationRepository context, ISalesCenterService salesCenterService)
        {
            _context = context;
            _SalesCenterService = salesCenterService;
        }

        public UserModel GetUserDetails(int UserId)
        {
            UserModel user = new UserModel();

            try
            {
                user = _context.GetUserByID(UserId);
                string UserRole = _context.GetRoleName(UserId);
                var SalesCenterNames = _SalesCenterService.GetSalesCenter(UserId).Select(x => x.SalesCenterName).ToList();

                if (user != null)
                {
                    user.UserRole = UserRole;
                    user.UserSalesCenter = SalesCenterNames;
                    return user;
                }
                else
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using OgmentoAPI.Domain.Authorization.Abstractions.Models;
using OgmentoAPI.Domain.Authorization.Abstractions.Repository;
using OgmentoAPI.Domain.Authorization.Abstractions.Services;
using OgmentoAPI.Domain.Client.Abstractions.Service;

namespace OgmentoAPI.Domain.Authorization.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthorizationRepository _context;
        private readonly ISalesCenterService _salesCenterService;
        public UserService(IAuthorizationRepository context, ISalesCenterService salesCenterService)
        {
            _context = context;
            _salesCenterService = salesCenterService;
        }

        public UserModel GetUserDetails(int UserId)
        {
            UserModel user = new UserModel();

            try
            {
                user = _context.GetUserByID(UserId);
                string userRole = _context.GetRoleName(UserId);
                var salesCenterNames = _salesCenterService.GetSalesCenterForUser(UserId).Select(x => x.SalesCenterName).ToList();

                if (user != null)
                {
                    user.UserRole = userRole;
                    user.UserSalesCenter =salesCenterNames;
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

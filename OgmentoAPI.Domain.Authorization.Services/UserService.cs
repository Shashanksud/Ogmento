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
                var salesCenterNames = _salesCenterService.GetSalesCenterForUser(UserId).ToList();

                Dictionary<Guid, string> salesCenterDictionary = salesCenterNames.ToDictionary(sc => sc.SalesCenterUid, sc => sc.SalesCenterName);

                if (user != null)
                {
                    user.UserRole = userRole;
                    user.SalesCenters = salesCenterDictionary;
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

        public int? UpdateUser(UserModel user)
        {
            try
            {
                List<Guid> guidList = new List<Guid>(user.SalesCenters.Keys);
                _salesCenterService.UpdateSalesCenters(user.UserId, guidList);
                return _context.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

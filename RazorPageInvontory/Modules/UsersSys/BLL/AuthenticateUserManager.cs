using RazorPageInvontory.Modules.UsersSys.DAL;
using RazorPageInvontory.Modules.UsersSys.Models;

namespace RazorPageInvontory.Modules.UsersSys.BLL
{
    public class AuthenticateUserManager
    {
        private readonly AuthenticateUserSer _authService;

        public AuthenticateUserManager(AuthenticateUserSer authService)
        {
            _authService = authService;
        }

        public async Task<AuthModel> AuthenticateUserAsync(string username, string password)
        {
            var request = new UserLoginRequest
            {
                username = username,
                password = password
            };

            var ss= await _authService.GetCustomersDataAsync();

            return await _authService.LoginAsync(request);
        }
    }
}

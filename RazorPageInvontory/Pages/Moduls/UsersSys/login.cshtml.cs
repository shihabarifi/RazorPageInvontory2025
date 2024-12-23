using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Modules.UsersSys.DAL;
using RazorPageInvontory.Modules.UsersSys.BLL;
using RazorPageInvontory.Modules.UsersSys.Models;

namespace RazorPageInvontory.Pages.Moduls.UsersSys
{
    public class loginModel : PageModel
    {
        private readonly AuthenticateUserManager _authManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //_userService = userService;
        public loginModel(AuthenticateUserManager authManager, IHttpContextAccessor httpContextAccessor)
        {
            _authManager = authManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _authManager.AuthenticateUserAsync(Username, Password);

            if (result != null && result.IsAuthenticated)
            {
                //  Œ“Ì‰ «· Êﬂ‰ ›Ì Session
                _httpContextAccessor.HttpContext.Session.SetString("Token", result.Token);

                // ≈⁄«œ…  ÊÃÌÂ ≈·Ï «·’›Õ… «·—∆Ì”Ì… »⁄œ «·‰Ã«Õ
                return Redirect("/Index");
            }

            ErrorMessage = result.Message;
            return Page();
        }
        public void OnGet()
        {
        }
    }
}

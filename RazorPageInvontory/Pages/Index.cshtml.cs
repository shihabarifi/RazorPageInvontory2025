using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Modules.UsersSys.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using RazorPageInvontory.Modules.UsersSys.DAL;
using RazorPageInvontory.Models;

namespace RazorPageInvontory.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AuthenticateUserSer authService;

        public IndexModel(ILogger<IndexModel> logger, AuthenticateUserSer authService)
        {
            _logger = logger;
            this.authService = authService;
        }
       
     

        public void OnGet()
        {


        }

       
    }
}

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

            //SomeMethod();
          //var cc=  OnGetGetcustomers();


        }

        // „À«· ⁄·Ï ﬂÌ›Ì… «” Œœ«„ «·œ«·…:
        public async Task<JsonResult> OnGetGetcustomers()
        {
            try
            {
                var customers = await authService.GetCustomersDataAsync();

                if (customers != null)
                {
                    // ·« œ«⁄Ì ·Õ·ﬁ… «· ﬂ—«— Â‰«.  „ Ã·» «·»Ì«‰«  »«·›⁄· ›Ì ﬁ«∆„….
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.ID}: {customer.CustomerName}"); // Ì„ﬂ‰ﬂ «·«Õ ›«Ÿ »Â–« «·”ÿ— ··  »⁄
                    }
                    return new JsonResult(customers);
                }
                else
                {
                    // «· ⁄«„· „⁄ Õ«·… ⁄œ„ Ã·» «·»Ì«‰«  »‰Ã«Õ »‘ﬂ· √›÷·
                    return new JsonResult(new { error = "·„ Ì „ Ã·» «·»Ì«‰«  »‰Ã«Õ" }) { StatusCode = 500 }; // ≈—Ã«⁄ ﬂ«∆‰ JSON „⁄ —„“ Õ«·… 500
                                                                                                             // √Ê Ì„ﬂ‰ﬂ «” Œœ«„:
                                                                                                             // return StatusCode(500, "·„ Ì „ Ã·» «·»Ì«‰«  »‰Ã«Õ");
                }
            }
            catch (Exception ex)
            {
                // „⁄«·Ã… √Ì √Œÿ«¡ √Œ—Ï ﬁœ  ÕœÀ
                return new JsonResult(new { error = $"ÕœÀ Œÿ√: {ex.Message}" }) { StatusCode = 500 }; // ≈—Ã«⁄ ﬂ«∆‰ JSON „⁄ —”«·… «·Œÿ√ Ê—„“ Õ«·… 500
                                                                                                      // √Ê Ì„ﬂ‰ﬂ «” Œœ«„:
                                                                                                      // return StatusCode(500, $"ÕœÀ Œÿ√: {ex.Message}");
            }
        }
       
    }
}

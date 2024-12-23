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

        // ���� ��� ����� ������� ������:
        public async Task<JsonResult> OnGetGetcustomers()
        {
            try
            {
                var customers = await authService.GetCustomersDataAsync();

                if (customers != null)
                {
                    // �� ���� ����� ������� ���. �� ��� �������� ������ �� �����.
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.ID}: {customer.CustomerName}"); // ����� �������� ���� ����� ������
                    }
                    return new JsonResult(customers);
                }
                else
                {
                    // ������� �� ���� ��� ��� �������� ����� ���� ����
                    return new JsonResult(new { error = "�� ��� ��� �������� �����" }) { StatusCode = 500 }; // ����� ���� JSON �� ��� ���� 500
                                                                                                             // �� ����� �������:
                                                                                                             // return StatusCode(500, "�� ��� ��� �������� �����");
                }
            }
            catch (Exception ex)
            {
                // ������ �� ����� ���� �� ����
                return new JsonResult(new { error = $"��� ���: {ex.Message}" }) { StatusCode = 500 }; // ����� ���� JSON �� ����� ����� ���� ���� 500
                                                                                                      // �� ����� �������:
                                                                                                      // return StatusCode(500, $"��� ���: {ex.Message}");
            }
        }
       
    }
}

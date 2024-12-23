using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Models;
using RazorPageInvontory.ServicesLayer;
using System.Text.Json;

namespace RazorPageInvontory.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ISalesInvoiceService _salesInvoiceService;

        public CreateModel(ISalesInvoiceService salesInvoiceService)
        {
            _salesInvoiceService = salesInvoiceService;
        }

        [BindProperty]
        public SalesInvoiceHeader Invoice { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                Invoice = await _salesInvoiceService.GetSalesInvoice(id.Value);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _salesInvoiceService.CreateSalesInvoice(Invoice);
            if (result)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }


        // دالة لجلب بيانات الأصناف الافتراضية
        public IActionResult OnGetItems()
        {
            var items = new List<object>
        {
            new { code = "001", name = "ماء" },
            new { code = "002", name = "عصير" },
            new { code = "003", name = "سجائر" }
        };

            return new JsonResult(items, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

    }
}

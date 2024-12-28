using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Modules.POSSys.BLL;
using RazorPageInvontory.Modules.POSSys.Models;
using RazorPageInvontory.Shared.BLLSharedALL;
using System.Net.Http;

namespace RazorPageInvontory.Pages.Moduls.POSSys
{

    public class POSViewModel : PageModel
    {
        private readonly POSManager _posManager;
        private readonly BLLSharedServices _bLLShared;
        public POSViewModel(POSManager posManager)
        {
            _posManager = posManager;
           
        }
        public List<SPSellInvoice> Invoices { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                Invoices = await _posManager.GetInvoiceListAsync();
            }
            catch (Exception ex)
            {
                // ������ ����� (����� ����á ��� ����� �������� ���.)
                Console.WriteLine($"Error fetching invoices: {ex.Message}");
                Invoices = new List<SPSellInvoice>(); // ����� ����� ����� ����� ����� �����
            }
        }

        public async Task<IActionResult> OnGetDeleteInvoice(int invoiceId)
        {
            var result = _posManager.DeleteInvoiceAsync(invoiceId).Result;
            if (result.Success)
            {
                return new JsonResult(new { result.Success, result.Message });
            }
            else
            {
                result.Message = "��� ��� ����� ��� ��������: ";
                return new JsonResult(new { result.Success, result.Message });
            }
         
        }

        //GetInvoiceDetails
        public async Task<IActionResult> OnGetGetInvoiceDetailsAsync(int invoiceId)
        {
            var invoice = await _posManager.GetInvoiceByIdAsync(invoiceId);
            var inv = invoice.InvoiceDetails;

            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }
            return new JsonResult(new { success = true, inv });
           // return new JsonResult(inv); // ����� ������ �������� ��� JSON

        }

    }
}

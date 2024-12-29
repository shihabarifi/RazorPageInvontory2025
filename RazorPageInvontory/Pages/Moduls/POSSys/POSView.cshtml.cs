using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageInvontory.Modules.POSSys.BLL;
using RazorPageInvontory.Modules.POSSys.Models;
using RazorPageInvontory.Shared.BLLSharedALL;
using System.Collections.Generic;
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
        public List<HeaderInvoice> Invoices { get; set; }
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
                Invoices = new List<HeaderInvoice>(); // ����� ����� ����� ����� ����� �����
            }
        }

    

        //GetInvoiceDetails
        public async Task<IActionResult> OnGetGetInvoiceDetailsAsync(int invoiceId)
        {

            List < InvoiceDetail> detail = new List<InvoiceDetail>();

            detail = await _posManager.GetInvoiceDetailsAsync(invoiceId);
            //var inv = invoice;

            if (detail == null)
            {
                return NotFound("Invoice not found.");
            }
            return new JsonResult(new { success = true, detail });
           // return new JsonResult(inv); // ����� ������ �������� ��� JSON

        }

        //��� ������ ��� �����
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

    }
}

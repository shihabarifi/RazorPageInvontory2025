using RazorPageInvontory.Models;
using RazorPageInvontory.Modules.POSSys.DAL;
using RazorPageInvontory.Modules.POSSys.Models;
using RazorPageInvontory.Modules.UsersSys.Models;
using System.Text.Json;

namespace RazorPageInvontory.Modules.POSSys.BLL
{
    public class POSManager
    {
        private readonly POSSer _posService;

        public POSManager(POSSer posService)
        {
            _posService = posService;
        }
       

        public async Task<List<SalePointUser>> SalesPointsAsync()
        {

            return await _posService.GetSalesPointsAsync();
        }
      
        public async Task<(bool Success, string Message)> SendInvoiceToApiAsync(SPSellInvoice invoice)
        {
            try
            {
                var response = await _posService.SendInvoiceToApiAsync(invoice);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<InvoiceResponse>(responseString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (result.Success, " تم اضافة فاتورة بيع مباشر بـID  :  " + result.id + " بنجاح");
                }
                else
                {
                    return (false, "حدث خطأ أثناء حفظ الفاتورة: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return (false, "حدث خطأ أثناء حفظ الفاتورة: " + ex.Message);
            }
        }



        public async Task<SPSellInvoice> GetInvoiceByIdAsync(int invoiceId)
        {
            return await _posService.GetInvoiceByIdAsync(invoiceId);
        }

        public class InvoiceResponse
        {
            public int id { get; set; }
            public bool Success { get; set; }
        }

    }

}

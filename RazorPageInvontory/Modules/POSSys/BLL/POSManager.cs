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

        
        public async Task<(bool Success, string Message)> SendInvoiceForEditToApiAsync(SPSellInvoice invoice)
        {
            try
            {
                var response = await _posService.SendInvoiceForEditToApiAsync(invoice);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<InvoiceResponse>(responseString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (result.Success, " تم تعديل  فاتورة بيع مباشر بـID  :  " + result.id + " بنجاح");
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



        public async Task<(bool Success, string Message)> DeleteInvoiceAsync(int invoiceid)
        {
            try
            {
                var response = await _posService.DeleteInvoiceAsync(invoiceid);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<InvoiceResponse>(responseString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (result.Success, " تم حذف  فاتورة بيع مباشر بـID  :  " + result.id + " بنجاح");
                }
                else
                {
                    return (false, "حدث خطأ أثناء حذف الفاتورة: " + response.ReasonPhrase);
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

        public async Task<List<InvoiceDetail>> GetInvoiceDetailsAsync(int invoiceId)
        {
            return await _posService.GetInvoiceDetailsAsync(invoiceId);
        }

        public async Task<List<HeaderInvoice>> GetInvoiceListAsync()
        {
            return await _posService.GetInvoiceListAsync();
        }

        public class InvoiceResponse
        {
            public int id { get; set; }
            public bool Success { get; set; }
        }

    }

}

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
        public async Task<List<CustomerInfo>> CustomerInfoAsync()
        {

            return await _posService.GetCustomersDataAsync();
        }

        public async Task<List<FundUser>> FundUserAsync()
        {

            return await _posService.GetFundUserDataAsync();
        }

        public async Task<List<SalePointUser>> SalesPointsAsync()
        {

            return await _posService.GetSalesPointsAsync();
        }
        public async Task<List<Store>> StoreAsync()
        {

            return await _posService.GetStoresAsync();
        }

        public async Task<List<Products>> ProductsAsync()
        {

            return await _posService.GetProductsAsync();
        }
        public async Task<List<Unit>> UnitsAsync(long ClassID)
        {

            return await _posService.GetUnitsByClassIdDataAsync(ClassID);
        }
        public async Task<(bool Success, string Message)> SendInvoiceToApiAsync(SPSellInvoice invoice)
        {
            try
            {
                var response = await _posService.SendInvoiceToApiAsync(invoice);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<(bool Success, string Message)>(responseString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return (result.Success, result.Message);
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

        }
}

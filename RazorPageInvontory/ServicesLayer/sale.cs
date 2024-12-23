using RazorPageInvontory.Models;

namespace RazorPageInvontory.ServicesLayer
{
    // SalesInvoice.Business/Services/Interfaces/ISalesInvoiceService.cs
    public interface ISalesInvoiceService
    {
        Task<bool> CreateSalesInvoice(SalesInvoiceHeader invoice);
        Task<SalesInvoiceHeader> GetSalesInvoice(int invoiceId);
    }

    // SalesInvoice.Business/Services/Implementation/SalesInvoiceService.cs
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly HttpClient _httpClient;

        public SalesInvoiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // للتجربة، سنقوم بإنشاء بيانات افتراضية
        public async Task<SalesInvoiceHeader> GetSalesInvoice(int invoiceId)
        {
            // في الحالة الحقيقية، سنقوم بجلب البيانات من API
            var invoice = new SalesInvoiceHeader
            {
                InvoiceId = 1001,
                InvoiceDate = DateTime.Now,
                Currency = "SAR",
                CustomerName = "أحمد محمد",
                Details = new List<SalesInvoiceDetail>
            {
                new SalesInvoiceDetail
                {
                    Id = 1,
                    ItemCode = "W001",
                    ItemName = "ماء",
                    Unit = "كرتون",
                    Quantity = 10,
                    Cost = 12.5m,
                    Discount = 0
                },
                new SalesInvoiceDetail
                {
                    Id = 2,
                    ItemCode = "J001",
                    ItemName = "عصير",
                    Unit = "علبة",
                    Quantity = 5,
                    Cost = 8.75m,
                    Discount = 2
                }
            }
            };

            return invoice;
        }
        public async Task<bool> CreateSalesInvoice(SalesInvoiceHeader invoice)
        {
            var response = await _httpClient.PostAsJsonAsync("api/salesinvoice", invoice);
            return response.IsSuccessStatusCode;
        }
    }
}

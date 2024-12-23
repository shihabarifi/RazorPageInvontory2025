namespace RazorPageInvontory.Models
{
    // SalesInvoice.Services/Models/SalesInvoiceHeader.cs
    public class SalesInvoiceHeader
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Currency { get; set; }
        public string CustomerName { get; set; }
        public List<SalesInvoiceDetail> Details { get; set; }
    }

    // SalesInvoice.Services/Models/SalesInvoiceDetail.cs
    public class SalesInvoiceDetail
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
    }
}

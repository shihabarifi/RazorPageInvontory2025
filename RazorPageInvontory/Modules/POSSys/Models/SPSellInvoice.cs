namespace RazorPageInvontory.Modules.POSSys.Models
{
    public class SPSellInvoice
    {
        public int ID { get; set; }
        public string? PeriodNumber { get; set; }
        public int SalePointID { get; set; }
        public string? TheNumber { get; set; }
        public string? TheDate { get; set; }
        public string? ThePay { get; set; }
        public int StoreID { get; set; }
        public int AccountID { get; set; }
        public string? CustomerName { get; set; }
        public string? Notes { get; set; }
        public int UserID { get; set; }
        public decimal Descount { get; set; }
        public decimal Debited { get; set; }
        public decimal PayAmount { get; set; }

        public List<SPSellInvoiceDetails> InvoiceDetails { get; set; } // علاقة مع التفاصيل
    }

    public class SPSellInvoiceDetails
    {
        public int ID { get; set; }
        public int ParentID { get; set; } // مفتاح خارجي يشير إلى الفاتورة الرئيسية
        public int ClassID { get; set; }
        public int UnitID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubDescount { get; set; }
        public decimal TotalAMount { get; set; }
    }
}

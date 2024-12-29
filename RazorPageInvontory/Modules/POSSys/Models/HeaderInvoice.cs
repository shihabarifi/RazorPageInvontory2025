namespace RazorPageInvontory.Modules.POSSys.Models
{
    public class HeaderInvoice
    {
        public int ID { get; set; }
        public string PeriodNumber { get; set; }
        public int SalePointID { get; set; }
        public string TheNumber { get; set; }
        public DateTime TheDate { get; set; }
        public string ThePay { get; set; }
        public string StoreName { get; set; }
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string Notes { get; set; }
        public decimal Descount { get; set; }
        public decimal Debited { get; set; }
        public decimal PayAmount { get; set; }
    }
}

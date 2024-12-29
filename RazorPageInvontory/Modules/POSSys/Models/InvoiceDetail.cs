namespace RazorPageInvontory.Modules.POSSys.Models
{
    public class InvoiceDetail
    {
        public string? ClassNumber { get; set; } // رقم الصنف
        public string? ClassName { get; set; }  // اسم الصنف
        public string? UnitName { get; set; }     // الوحدة
        public string? Quantity { get; set; }        // الكمية
        public string? UnitPrice { get; set; }     // سعر الوحدة
        public string? SubDescount { get; set; }  // الخصم
        public string? TotalAmount { get; set; }  // الإجمالي
    }
}

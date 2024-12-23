using System.ComponentModel.DataAnnotations;

namespace RazorPageInvontory.Modules.POSSys.Models
{
    public class SalePointUser
    {
        public int SalePointID { get; set; }

        [Required(ErrorMessage = "رقم نقطة البيع مطلوب")]
        [StringLength(50, ErrorMessage = "رقم نقطة البيع يجب أن يكون أقل من 50 حرفًا")] // تعديل طول السلسلة حسب الحاجة
        public string? SalePointNumber { get; set; }
    }
}

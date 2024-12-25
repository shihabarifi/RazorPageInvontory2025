using System.ComponentModel.DataAnnotations;

namespace RazorPageInvontory.Models
{
    public class Unit
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "اسم الوحدة مطلوب")]
        [StringLength(255, ErrorMessage = "اسم الوحدة يجب أن يكون أقل من 255 حرفًا")]
        public string? UnitName { get; set; }

        public double ExchangeFactor { get; set; } // أو double حسب نوع البيانات في قاعدة البيانات
        public double PartingPrice { get; set; } // أو double حسب نوع البيانات في قاعدة البيانات
    }
}

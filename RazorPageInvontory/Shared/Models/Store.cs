using System.ComponentModel.DataAnnotations;

namespace RazorPageInvontory.Models
{
    public class Store
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "اسم المخزن مطلوب")]
        [StringLength(255, ErrorMessage = "اسم المخزن يجب أن يكون أقل من 255 حرفًا")]
        public string? StoreName { get; set; }
    }
}

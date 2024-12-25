using System.ComponentModel.DataAnnotations;

namespace RazorPageInvontory.Models
{
    public class Products
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "اسم الصف مطلوب")]
        [StringLength(255, ErrorMessage = "اسم الصف يجب أن يكون أقل من 255 حرفًا")]
        public string? ClassName { get; set; }
    }
}

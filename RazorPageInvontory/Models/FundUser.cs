using System.ComponentModel.DataAnnotations;

namespace RazorPageInvontory.Models
{
    public class FundUser
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "اسم الصندوق مطلوب")]
        [StringLength(255, ErrorMessage = "اسم الصندوق يجب أن يكون أقل من 255 حرفًا")]
        public string? FundName { get; set; }
    }
}

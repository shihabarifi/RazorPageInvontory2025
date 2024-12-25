using System.ComponentModel.DataAnnotations;

namespace RazorPageInvontory.Models
{
    public class TokenRequestModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

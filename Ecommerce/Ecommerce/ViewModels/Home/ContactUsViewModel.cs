using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.Home
{
    public class ContactUsViewModel
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.Users
{
    public class CreateUserViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Required")]
        public string User { get; set; }

        [Display(Name = "Mail")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "InvalidFormat")]
        public string Email { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Required")]
        public string Surname { get; set; }

        [Display(Name = "Es administrador")]
        public bool IsAdmin { get; set; }
        
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

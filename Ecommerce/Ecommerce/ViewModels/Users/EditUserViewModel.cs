using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.Users
{
    public class EditUserViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Required")]
        public string User { get; set; }

        [Display(Name = "Mail")]
        [EmailAddress(ErrorMessage = "Formato inválido")]
        public string Email { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Required")]
        public string Surname { get; set; }
        
        [Display(Name = "Es administrador")]
        public bool IsAdmin { get; set; }
    }
}

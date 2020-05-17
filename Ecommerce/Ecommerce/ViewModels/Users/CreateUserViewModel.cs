using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Users
{
    public class CreateUserViewModel
    {
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
    }
}

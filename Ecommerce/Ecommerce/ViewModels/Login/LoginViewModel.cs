using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Login
{
    public class LoginViewModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo 'Email' es obligatorio.")]
            [EmailAddress(ErrorMessage = "El 'Email' no es una dirección de Email válida.")]
            public string Email { get; set; }
            [Required(ErrorMessage = "El campo 'Contraseña' es obligatorio.")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "El número de caracteres del {0} debe ser al menos {2}.", MinimumLength = 6)]
            public string Password { get; set; }
            [Required(ErrorMessage = "El campo 'Usuario' es obligatorio.")]
            public string User { get; set; }
            [Required(ErrorMessage = "El campo 'Apellido' es obligatorio.")]
            public string Surname { get; set; }
            [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
            public string Name { get; set; }

        }
    }
}

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
            [Required(ErrorMessage = "<font color='red'>El campo 'Email' es obligatorio.</font>")]
            [EmailAddress(ErrorMessage = "<font color='red'>El 'Email' no es una dirección de Email válida.</font>")]
            public string Email { get; set; }
            [Required(ErrorMessage = "<font color='red'>El campo 'Contraseña' es obligatorio.</font>")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "<font color='red'>El número de caracteres del {0} debe ser al menos {2}.</font>", MinimumLength = 6)]
            public string Password { get; set; }
            [Required(ErrorMessage = "<font color='red'>El campo 'Usuario' es obligatorio.</font>")]
            public string User { get; set; }
            [Required(ErrorMessage = "<font color='red'>El campo 'Apellido' es obligatorio.</font>")]
            public string Surname { get; set; }
            [Required(ErrorMessage = "<font color='red'>El campo 'Nombre' es obligatorio.</font>")]
            public string Name { get; set; }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.Login
{
    public class LoginViewModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "El 'Email' no es una dirección de Email válida.")]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            public string User { get; set; }
            [Required]
            public string Surname { get; set; }
            [Required]
            public string Name { get; set; }

        }
    }
}

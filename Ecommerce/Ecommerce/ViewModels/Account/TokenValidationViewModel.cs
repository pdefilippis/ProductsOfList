using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Account
{
    public class TokenValidationViewModel
    {
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Token { get; set; }
    }
}

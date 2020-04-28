using Microsoft.AspNetCore.WebSockets.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        // English, >0 a-z, >0 A-Z, >0 digit, >0 special, len >= 8
        public const string PasswordComplexity = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!-/:-@\[-`{-~]).{8,}$";

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Contraseña actual")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(PasswordComplexity, ErrorMessage = "PasswordComplexityWarning")]
        [Display(Name = "Contraseña nueva")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [Compare("NewPassword", ErrorMessage = "PasswordConfirmationMatchWarning")]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirmation { get; set; }
    }
}

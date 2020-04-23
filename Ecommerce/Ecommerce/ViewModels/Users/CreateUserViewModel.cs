using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Required")]
        public string Username { get; set; }

        [Display(Name = "Mail")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "InvalidFormat")]
        public string Mail { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Required")]
        public string Surname { get; set; }
    }
}

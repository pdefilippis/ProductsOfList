using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Account
{
    public class UserDataViewModel
    {
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        public string Surname { get; set; }

        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Display(Name = "Mail")]
        public string Email { get; set; }

        [Display(Name = "Creado")]
        public string CreationTimestamp { get; set; }
    }
}

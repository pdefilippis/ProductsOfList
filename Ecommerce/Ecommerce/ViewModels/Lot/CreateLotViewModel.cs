using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Lot
{
    public class CreateLotViewModel
    {
        [MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
        public string Descripcion { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile Imagen { get; set; }

        public int LotId { get; set; }
    }
}

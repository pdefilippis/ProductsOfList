using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Article
{
    public class CreateArticleViewModel
    {
        public int LotId { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo 'Tipo' es obligatorio.")]
        public int TypeId { get; set; }
        [MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo 'Marca' es obligatorio.")]
        //public string Brand { get; set; }
        //[MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        //[Display(Name = "Número de serie")]
        //[Required(ErrorMessage = "El campo 'Número de serie' es obligatorio.")]
        public string SerialNumber { get; set; }
        [MaxLength(50, ErrorMessage = "Permite hasta 50 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
        public string Description { get; set; }
        public List<SelectListItem> Types { get; set; }
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo 'Precio' es obligatorio.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Debe ingresar números mayor a cero")]
        public decimal Price { get; set; }
    }
}

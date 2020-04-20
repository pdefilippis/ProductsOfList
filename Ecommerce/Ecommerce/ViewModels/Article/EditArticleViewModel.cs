using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.Tables.Article;

namespace Ecommerce.ViewModels.Article
{
    public class EditArticleViewModel
    {
        [Display(Name = "Tipo")]
        public int TypeId { get; set; }

        [MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo 'Marca' es obligatorio.")]
        public string Brand { get; set; }
        [MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        [Display(Name = "Número de serie")]
        [Required(ErrorMessage = "El campo 'Número de serie' es obligatorio.")]
        public string SerialNumber { get; set; }
        [MaxLength(50, ErrorMessage = "Permite hasta 50 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
        public string Description { get; set; }
        public ArticleState State { get; set; }
        public int Lot_ID { get; set; }
        public List<SelectListItem> Types { get; set; }
        [Display(Name = "ID")]
        public int ArticleId { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo 'Precio' es obligatorio.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Debe ingresar números mayor a cero")]
        public int? Price { get; set; }
    }
}

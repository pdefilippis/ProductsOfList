namespace Ecommerce.ViewModels
{
    public class CreateLotViewModel
    {
        [MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile Image { get; set; }

        public int LotId { get; set; }
    }
}
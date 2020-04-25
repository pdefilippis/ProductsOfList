namespace Ecommerce.ViewModels
{
    public class EditLotViewModel
    {
        [MaxLength(30, ErrorMessage = "Permite hasta 30 caracteres")]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
        public string Description { get; set; }

        public int LotId { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        public bool FlagImage { get; set; }
    }
}
using Ecommerce.Domain.Models;

namespace Ecommerce
{
    public class IndexPublicViewModel
    {
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public int TakenId { get; set; }        
        public Lote.EstadoLote Estado { get; set; }
    }
}
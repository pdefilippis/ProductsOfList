using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Lote
    {
        public Lote()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }
        [Column(TypeName = "image")]
        public byte[] Imagen { get; set; }
        [StringLength(50)]
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        [InverseProperty("IdLoteNavigation")]
        public virtual ICollection<Articulo> Articulo { get; set; }
        public EstadoLote Estado { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }

        public enum EstadoLote
        {
            [Display(Name = "INACTIVO")]
            DISABLED = 0,
            [Display(Name = "ACTIVO")]
            ENABLED = 1,
            [Display(Name = "CERRADO")]
            CLOSED = 2,
        }
    }
}

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
        [Column(TypeName = "datetime")]
        public DateTime Creacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Actualizacion { get; set; }

        [InverseProperty("IdLoteNavigation")]
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}

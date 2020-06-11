using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Marca
    {
        public Marca()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }
        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required]
        public bool? Activo { get; set; }

        [InverseProperty("IdMarcaNavigation")]
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class ArticuloTipo
    {
        public ArticuloTipo()
        {
            Articulo = new HashSet<Articulo>();
        }

        public int Id { get; set; }
        [StringLength(20)]
        public string Codigo { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        [InverseProperty("IdTipoNavigation")]
        public virtual ICollection<Articulo> Articulo { get; set; }
    }
}

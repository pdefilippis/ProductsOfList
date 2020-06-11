using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Notificaciones
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdArticulo { get; set; }
        public bool? Leido { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Stamp { get; set; }

        [ForeignKey("IdArticulo")]
        [InverseProperty("Notificaciones")]
        public virtual Articulo IdArticuloNavigation { get; set; }
        [ForeignKey("IdUsuario")]
        [InverseProperty("Notificaciones")]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}

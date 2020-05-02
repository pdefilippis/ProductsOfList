using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Solicitud
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdArticulo { get; set; }

        [ForeignKey("IdArticulo")]
        [InverseProperty("Solicitud")]
        public virtual Articulo IdArticuloNavigation { get; set; }
        [ForeignKey("IdUsuario")]
        [InverseProperty("Solicitud")]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}

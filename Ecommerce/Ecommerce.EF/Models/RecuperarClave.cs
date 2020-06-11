using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class RecuperarClave
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Stamp { get; set; }
        [Required]
        [StringLength(6)]
        public string Token { get; set; }

        [ForeignKey("IdUsuario")]
        [InverseProperty("RecuperarClave")]
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}

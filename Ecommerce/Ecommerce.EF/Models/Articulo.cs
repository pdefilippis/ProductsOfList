using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            Notificaciones = new HashSet<Notificaciones>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int Id { get; set; }
        public int IdTipo { get; set; }
        [Required]
        [StringLength(30)]
        public string NumeroSerie { get; set; }
        [StringLength(70)]
        public string Descripcion { get; set; }
        public int IdLote { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }
        public int? UsuarioAdjudicado { get; set; }
        public bool Activo { get; set; }
        [StringLength(100)]
        public string Marca { get; set; }

        [ForeignKey("IdLote")]
        [InverseProperty("Articulo")]
        public virtual Lote IdLoteNavigation { get; set; }
        [ForeignKey("IdTipo")]
        [InverseProperty("Articulo")]
        public virtual ArticuloTipo IdTipoNavigation { get; set; }
        [ForeignKey("UsuarioAdjudicado")]
        [InverseProperty("Articulo")]
        public virtual Usuario UsuarioAdjudicadoNavigation { get; set; }
        [InverseProperty("IdArticuloNavigation")]
        public virtual ICollection<Notificaciones> Notificaciones { get; set; }
        [InverseProperty("IdArticuloNavigation")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}

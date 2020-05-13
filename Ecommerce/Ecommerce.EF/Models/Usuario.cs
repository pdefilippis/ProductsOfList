using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Articulo = new HashSet<Articulo>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int Id { get; set; }
        [Column("Usuario")]
        [StringLength(50)]
        public string Usuario1 { get; set; }
        [Required]
        [StringLength(200)]
        public string Clave { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Creacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UltimoIngreso { get; set; }
        [StringLength(100)]
        public string Mail { get; set; }
        public bool Administrador { get; set; }
        [Required]
        public bool? Activo { get; set; }

        [InverseProperty("UsuarioAdjudicadoNavigation")]
        public virtual ICollection<Articulo> Articulo { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}

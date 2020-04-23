using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        [Column("Usuario")]
        [StringLength(50)]
        public string Usuario1 { get; set; }
        [StringLength(50)]
        public string Clave { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        public EstadoUsuario State { get; set; }
        public DateTime CreationTimestamp { get; set; } = DateTime.Now; //Fecha Creacion
        public DateTime? LastLoginTimestamp { get; set; } //Ultimo ingreso
        
        public enum EstadoUsuario
        {
            [Display(Name = "USER_STATE_NOT_ENABLED")]
            NOT_ENABLED = 0,
            [Display(Name = "USER_STATE_ENABLED")]
            ENABLED = 10
        }
    }
}

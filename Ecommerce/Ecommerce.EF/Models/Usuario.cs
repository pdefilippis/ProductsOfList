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
    }
}

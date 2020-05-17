using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Lote = new HashSet<Lote>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        [Required]
        public bool? Activo { get; set; }

        [InverseProperty("IdEstadoNavigation")]
        public virtual ICollection<Lote> Lote { get; set; }
    }
}

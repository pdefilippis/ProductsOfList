using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class LoteHistorial
    {
        public int LotHistoryId { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
        public virtual Lote Lote { get; set; }
        public virtual Usuario Usuario { get; set; }
        public AccionLote Action { get; set; }
        public DateTime Date { get; set; }

        public enum AccionLote
        {
            [Display(Name = "CREAR")]
            CREATE = 1,
            [Display(Name = "EDITAR")]
            EDIT = 2,
            [Display(Name = "CERRAR")]
            CLOSE = 3,
            [Display(Name = "ACTIVAR")]
            ENABLE = 4,
            [Display(Name = "INACTIVAR")]
            DISABLE = 5,
        }
    }
}

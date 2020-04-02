using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Tables
{
    public class LotHistory
    {
        public int LotHistoryId { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual User User { get; set; }
        public LotAction Action { get; set; }
        public DateTime Date { get; set; }

        public enum LotAction
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

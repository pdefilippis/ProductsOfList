using Ecommerce.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Lot
    {
        public int LotId { get; set; }
        public string Description { get; set; }
        public LotState State { get; set; }
        public byte[] Image { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual List<Article> Articles { get; set; }
        public enum LotState
        {
            [Display(Name = "INACTIVO")]
            DISABLED = 0,
            [Display(Name = "ACTIVO")]
            ENABLED = 1,
            [Display(Name = "CERRADO")]
            CLOSED = 2,
        }
    }
}
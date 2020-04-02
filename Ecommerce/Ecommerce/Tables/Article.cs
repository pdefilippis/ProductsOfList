using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Tables
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int? AdjudicatedUserId { get; set; }
        public virtual ArticleType Type { get; set; }
        public int TypeId { set; get; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public ArticleState State { get; set; }
        public virtual Lot Lot { get; set; }
        public string Description { get; set; }
        public int LotId { get; set; }
        public virtual ICollection<UserArticle> UserArticles { get; set; }
        public virtual User AdjudicatedUser { get; set; }
        public int Price { get; set; }

        public enum ArticleState
        {
            [Display(Name = "INACTIVO")]
            NOT_ENABLED = 0,
            [Display(Name = "ACTIVO")]
            ENABLED = 1,
            [Display(Name = "CERRADO")]
            CLOSED = 2
        }
    }
}

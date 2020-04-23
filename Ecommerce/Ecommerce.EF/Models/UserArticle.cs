using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Models
{
    public class UserArticle
    {
        public int UserId { get; set; }
        public virtual Usuario User { get; set; }
        public int ArticleId { get; set; }
        public virtual Articulo Articulo { get; set; }
    }
}

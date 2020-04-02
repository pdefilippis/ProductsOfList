using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.Tables.Article;

namespace Ecommerce.ViewModels.Article
{
    public class EditArticleViewModel
    {
        [Display(Name = "Tipo")]
        public int TypeId { get; set; }
        
        public string Brand { get; set; }
        
        public string SerialNumber { get; set; }
       
        public string Description { get; set; }
        public ArticleState State { get; set; }
        public int Lot_ID { get; set; }
        public List<SelectListItem> Types { get; set; }

        public int ArticleId { get; set; }
        
        public int? Price { get; set; }
    }
}

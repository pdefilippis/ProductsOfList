using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Article
{
    public class CreateArticleViewModel
    {
        public int LotId { get; set; }

        public int TypeId { get; set; }

        public string Brand { get; set; }

        public string SerialNumber { get; set; }

        public string Description { get; set; }
        public List<SelectListItem> Types { get; set; }

        public int? Price { get; set; }

    }
}

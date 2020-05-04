using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels.Error
{
    public class StatusViewModel
    {
        public int HTTPStatusCode { get; set; }
        public string Message { get; set; }
    }
}

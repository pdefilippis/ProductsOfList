using Ecommerce.Helpers;
using Ecommerce.Tables;
using Ecommerce.ViewModels.Lot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.Lot;
using static Ecommerce.Tables.LotHistory;

namespace Ecommerce.Controllers
{
    public class LotController : Controller
    {
        public IActionResult CreateLot()
        {
            return View();
        }

        public IActionResult EditLot()
        {
            return View();
        }
    }
}

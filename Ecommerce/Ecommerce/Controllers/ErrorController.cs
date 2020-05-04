using Ecommerce.ViewModels.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View(new IndexViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        //[Route("[controller]/[action]/{code}")]
        public IActionResult Status()
        {
            return View(new StatusViewModel { HTTPStatusCode = 500, Message = "Ups detectamos un error!!!" });
        }
    }
}

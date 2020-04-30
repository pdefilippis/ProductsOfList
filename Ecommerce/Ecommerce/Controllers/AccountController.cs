using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult UserData()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}

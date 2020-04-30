using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
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

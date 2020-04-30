using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        public IActionResult EditUser()
        {
            return View();
        }

        public IActionResult EnableUserConfirmation()
        {
            return View();
        }

        public IActionResult DisableUserConfirmation()
        {
            return View();
        }
    }
}

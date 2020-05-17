using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult UserData()
        {
            //var CurrentUserName = int.Parse(HttpContext.User.Identity.Name);

            //var user = _usuarioManager.Get();

            //var _user = user.First(u => u.Id == CurrentUserName);

            //var userDataViewModel = new UserDataViewModel
            //{
            //    Name = _user.Nombre,
            //    Surname = _user.Apellido,
            //    User = _user.UserName,
            //    Email = _user.Email,
            //    CreationTimestamp = _user.Creacion.ToString(),
            //};
            
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}

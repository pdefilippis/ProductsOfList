using Ecommerce.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly Core.IUsuarioManager _usuarioManager;

        public AccountController(Core.IUsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }

        public IActionResult UserData()
        {
            var CurrentUserName = HttpContext.User.Identity.Name;

            var user = _usuarioManager.GetByName(CurrentUserName);

            var userDataViewModel = new UserDataViewModel
            {
                Name = user.Nombre,
                Surname = user.Apellido,
                User = user.UserName,
                Email = user.Email,
                CreationTimestamp = user.Creacion.ToString(),
            };

            return View(userDataViewModel);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}

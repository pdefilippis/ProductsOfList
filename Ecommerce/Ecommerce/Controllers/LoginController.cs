using Ecommerce.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Common.DataMembers.Input;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        private readonly Core.IUsuarioManager _usuarioManager;

        public LoginController(Core.IUsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel usuario)
        {
            _usuarioManager.Login(new Usuario
            {
                Password = usuario.Input.Password,
                UserName = usuario.Input.User
            });
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

    }
}
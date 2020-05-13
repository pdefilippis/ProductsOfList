using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly Core.IUsuarioManager _usuarioManager;

        public UserController(Core.IUsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            var user = _usuarioManager.GetAll();

            var item = user.Select(u => new
            {
                user_id = u.Id,
                username = u.UserName,
                name = u.Nombre,
                surname = u.Apellido,
                mail = u.Email,
                creation_timestamp = u.Creacion.ToString("dd/MM/yyyy HH:mm:ss"),
                last_login_timestamp = u.UltimoIngreso.ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            return Json(item);
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

using Ecommerce.Domain.Models;
using Ecommerce.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Usuario = Ecommerce.Common.DataMembers.Input.Usuario;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ProductsManagerContext _context;
        private readonly Core.IUsuarioManager _usuarioManager;

        public UserController(Core.IUsuarioManager usuarioManager, ProductsManagerContext context)
        {
            _usuarioManager = usuarioManager;
            _context = context;
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
                isadmin = u.EsAdministrador,
                enabled = u.Activo,
                creation_timestamp = u.Creacion.ToString("dd/MM/yyyy HH:mm:ss"),
                last_login_timestamp = u.UltimoIngreso.ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            return Json(item);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(CreateUserViewModel viewModel)
        {
           
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Nombre = viewModel.Name,
                    Apellido = viewModel.Surname,
                    UserName = viewModel.User,
                    Email = viewModel.Email,
                    EsAdministrador = viewModel.IsAdmin,
                    Password = null
                };

                _usuarioManager.Register(usuario);

                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult EditUser(int UserId)
        {
            var usuario = _usuarioManager.GetById(UserId);
            if (usuario == null)
                return RedirectToAction("Index");
            else
                return View(new EditUserViewModel
                {
                    UserId = UserId,
                    User = usuario.UserName,
                    Name = usuario.Nombre,
                    Surname = usuario.Apellido,
                    Email = usuario.Email,
                    IsAdmin = usuario.EsAdministrador
                });
        }


        [HttpPost]
        public IActionResult EditUser(EditUserViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                _usuarioManager.Save(new Usuario {
                    Apellido = viewModel.Surname,
                    Email = viewModel.Email,
                    EsAdministrador = viewModel.IsAdmin,
                    Nombre = viewModel.Name,
                    UserName =viewModel.User,
                    Id = viewModel.UserId
                });

                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }

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

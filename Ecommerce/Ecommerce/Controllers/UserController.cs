using Ecommerce.Domain.Models;
using Ecommerce.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Usuario = Ecommerce.Common.DataMembers.Input.Usuario;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ProductsManagerContext _context;
        private readonly Core.IUsuarioManager _usuarioManager;
        private readonly ILogger<UserController> _logger;

        public UserController(Core.IUsuarioManager usuarioManager, ProductsManagerContext context, ILogger<UserController> logger)
        {
            _usuarioManager = usuarioManager;
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            var user = _usuarioManager.GetAll();
            var currentUserId = _usuarioManager.GetByName(HttpContext.User.Identity.Name).Id;

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
                last_login_timestamp = u.UltimoIngreso.ToString("dd/MM/yyyy HH:mm:ss"),
                current_user_id = currentUserId
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
            try
            {
                var userValidator = _usuarioManager.GetAll();
                if (viewModel.User == null || viewModel.Email == null || viewModel.Name == null || viewModel.Surname == null
                    || viewModel.Password == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                }
                else
                {
                    if (userValidator.Any(u => u.UserName.ToLower() == viewModel.User.ToLower()))
                        ModelState.AddModelError("", "Ya existe un usuario con este nombre de usuario");
                    if (userValidator.Any(u => u.Email.ToLower() == viewModel.Email.ToLower()))
                        ModelState.AddModelError("", "Ya existe un usuario con este mismo Email");
                }
                

                if (ModelState.IsValid)
                {
                    var usuario = new Usuario
                    {
                        Nombre = viewModel.Name,
                        Apellido = viewModel.Surname,
                        UserName = viewModel.User,
                        Email = viewModel.Email,
                        EsAdministrador = viewModel.IsAdmin,
                        Password = viewModel.Password,
                    };

                    TempData["SuccesMessage"] = "Se ha dado de alta un usuario";

                    _usuarioManager.Register(usuario);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }            
        }

        [HttpGet]
        public IActionResult EditUser(int UserId)
        {
            var usuario = _usuarioManager.GetById(UserId);

            try
            {
                if (usuario == null)
                    return RedirectToAction("Index");
                else
                    return View(new EditUserViewModel
                    {
                        UserId = UserId,
                        Name = usuario.Nombre,
                        Surname = usuario.Apellido,
                        Email = usuario.Email,
                        IsAdmin = usuario.EsAdministrador,
                        User = usuario.UserName
                    });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }


        [HttpPost]
        public IActionResult EditUser(EditUserViewModel viewModel)
        {
            try
            {
                var userValidator = _usuarioManager.GetAll();
                if (viewModel.Email == null || viewModel.Name == null || viewModel.Surname == null || viewModel.User == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                }
                else
                {
                    if (userValidator.Any(u => u.Id != viewModel.UserId && u.UserName.ToLower() == viewModel.User.ToLower()))
                        ModelState.AddModelError("", "Ya existe un usuario con este nombre de usuario");
                    if (userValidator.Any(u => u.Id != viewModel.UserId && u.Email.ToLower() == viewModel.Email.ToLower()))
                        ModelState.AddModelError("", "Ya existe un usuario con este mismo Email");
                }

                if (ModelState.IsValid)
                {
                    _usuarioManager.Save(new Usuario
                    {
                        Apellido = viewModel.Surname,
                        Email = viewModel.Email,
                        EsAdministrador = viewModel.IsAdmin,
                        Nombre = viewModel.Name,
                        Id = viewModel.UserId,
                        UserName = viewModel.User
                    });

                    TempData["SuccesMessage"] = "El usuario se ha editado correctamente";

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        public IActionResult EnableUser(int UserId)
        {
            var user = _usuarioManager.GetById(UserId);

            try
            {
                if (user.Activo == false)                                    
                    _usuarioManager.Enable(UserId);
                    TempData["SuccesMessage"] = "El usuario se ha activado";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        public IActionResult DisableUser(int UserId)
        {

            var user = _usuarioManager.GetById(UserId);

            try
            {
                if (user.Activo == true)
                    _usuarioManager.Disable(UserId);
                    TempData["ErrorMessage"] = "El usuario se ha desactivado";
                
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        public IActionResult EnableUserConfirmation(int UserId)
        {
            var user = _usuarioManager.GetById(UserId);

            try
            {
                if (user.Activo == true)
                    return RedirectToAction("Index");
                else
                    return View(new EnableUserConfirmationViewModel
                    {
                        UserId = UserId,
                        Username = user.UserName
                    });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        public IActionResult DisableUserConfirmation(int UserId)
        {
            var user = _usuarioManager.GetById(UserId);

            try
            {
                if (user.Activo == false)
                    return RedirectToAction("Index");
                else
                    return View(new DisableUserConfirmationViewModel
                    {
                        UserId = UserId,
                        Username = user.UserName
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }            
        }
    }
}

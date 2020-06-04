using Ecommerce.Common.DataMembers.Input;
using Ecommerce.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly Core.IUsuarioManager _usuarioManager;
        private readonly ILogger<AccountController> _logger;


        public AccountController(Core.IUsuarioManager usuarioManager, ILogger<AccountController> logger)
        {
            _usuarioManager = usuarioManager;
            _logger = logger;
        }

        public IActionResult UserData()
        {
            try
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
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            var CurrentUserName = HttpContext.User.Identity.Name;
            

            if (ModelState.IsValid)
            {

                if(viewModel.NewPassword == viewModel.NewPasswordConfirmation)
                {
                    var user = _usuarioManager.ChangePassword(new ChangePassword
                    {
                        OldPassword = Ecommerce.Common.Password.EncryptPassword(viewModel.OldPassword),
                        NewPassword = Ecommerce.Common.Password.EncryptPassword(viewModel.NewPassword),
                        UserName = CurrentUserName
                    });

                    var usuario = new Usuario
                    {
                        Id = user.Id,
                        Password = user.Password,
                        Apellido = user.Apellido,
                        Email = user.Email,
                        Nombre = user.Nombre,
                        UserName = user.UserName,
                        EsAdministrador = user.EsAdministrador
                    };

                    _usuarioManager.Save(usuario);

                }

                return RedirectToAction("Index","Login");
            }

            ModelState.AddModelError("", "Todos los campos deben ser completados");

            return View();
        }
    }
}

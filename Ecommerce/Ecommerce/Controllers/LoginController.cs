using Ecommerce.Common.DataMembers.Input;
using Ecommerce.ViewModels.Account;
using Ecommerce.ViewModels.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly Core.IUsuarioManager _usuarioManager;
        private readonly ILogger<LoginController> _logger;

        public LoginController(Core.IUsuarioManager usuarioManager, ILogger<LoginController> logger)
        {
            _usuarioManager = usuarioManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(LoginViewModel usuario)
        {

            try
            {
                var loginValidator = _usuarioManager.GetAll();
                var validateActivity = _usuarioManager.Get();

                var userValidado = validateActivity.FirstOrDefault(u => u.UserName == usuario.Input.User);

                
                if (usuario.Input.User == null || usuario.Input.Password == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                    return View();
                }

                if (!loginValidator.Any(r => r.UserName.ToLower() == usuario.Input.User || r.Password == usuario.Input.Password))
                {
                    ModelState.AddModelError("", "El usuario no existe");
                    return View();
                }

                if (userValidado.Activo != true)
                {
                    ModelState.AddModelError("", "Usuario inhabilitado");
                    return View();
                }

                if (loginValidator.Any(l => l.UserName.ToLower() != usuario.Input.User.ToLower() ||
                                            l.Password != Ecommerce.Common.Password.EncryptPassword(usuario.Input.Password)))
                {
                    ModelState.AddModelError("", "Usuario y/o contraseña incorrectos");
                    return View();
                }

                



                var user = _usuarioManager.Login(new Usuario
                {
                    Password = usuario.Input.Password,
                    UserName = usuario.Input.User
                });

                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.UserName),
                    new Claim(ClaimTypes.Role, user.EsAdministrador ? "ADMIN" : "CLIENT")
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new LoginViewModel());
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(LoginViewModel registerModel)
        {
            try
            {
                var registerValidator = _usuarioManager.GetAll();

                if (registerModel.Input.Password == null || registerModel.Input.Surname == null || registerModel.Input.Name == null
                     || registerModel.Input.User == null || registerModel.Input.Email == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                    return View();
                }
                else
                {
                    if (registerValidator.Any(r => r.Email.ToLower() == registerModel.Input.Email.ToLower()))
                        ModelState.AddModelError("", "Ya existe un usuario con este email");
                    if(registerValidator.Any(r => r.UserName.ToLower() == registerModel.Input.User.ToLower()))
                        ModelState.AddModelError("", "Ya existe un usuario con este nombre de usuario");
                }

                if (ModelState.IsValid)
                {
                    var user = _usuarioManager.Register(new Usuario
                    {
                        Password = registerModel.Input.Password,
                        Apellido = registerModel.Input.Surname,
                        Nombre = registerModel.Input.Name,
                        UserName = registerModel.Input.User,
                        Email = registerModel.Input.Email
                    });

                    return View("Index");
                }

                

                return View(registerModel);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            try
            {
                var resetpasswdValidator = _usuarioManager.GetAll();

                if (resetPassword.Email == null)
                {
                    ModelState.AddModelError("", "El correo es obligatorio");
                }
                else
                {
                    if(!resetpasswdValidator.Any(r => r.Email.ToLower() == resetPassword.Email))
                        ModelState.AddModelError("", "El correo es inválido o no existe");
                }


                if (ModelState.IsValid)
                {
                    _usuarioManager.RecoverPasswordNotification(resetPassword.Email);
                    return View("TokenValidation", new TokenValidationViewModel { Email = resetPassword.Email });
                }

                return View(resetPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
            
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        [AllowAnonymous]
        public IActionResult TokenValidation(ResetPasswordViewModel resetPassword)
        {
            return View(resetPassword);
        }

        [HttpPost, AllowAnonymous]
        public IActionResult TokenValidation(TokenValidationViewModel resetPassword)
        {
            try
            {
                var tokenValidation = _usuarioManager.GetAll();

                if (resetPassword.Email == null || resetPassword.Password == null || resetPassword.Token == null)
                {
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                }

                if (ModelState.IsValid && resetPassword.Password == resetPassword.ConfirmPassword)
                {
                    var result = _usuarioManager.CheckRecoverPassword(new RecoverPassword
                    {
                        Email = resetPassword.Email,
                        Password = resetPassword.Password,
                        Token = resetPassword.Token
                    });

                    return result ? View("Index") : View(resetPassword);
                }

                return View(resetPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

    }
}

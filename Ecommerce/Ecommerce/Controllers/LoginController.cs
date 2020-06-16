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
using System.Security.Claims;
using FaultContracts = Ecommerce.Common.FaultContracts;

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

                ModelState.AddModelError("", "Todos los campos deben ser completados");

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

                ModelState.AddModelError("", "Todos los campos deben ser completados");

                return View(registerModel);
            }
            catch(FaultContracts.InvalidDataException ex)
            {
                ViewBag.Errores = ex.Errores;
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

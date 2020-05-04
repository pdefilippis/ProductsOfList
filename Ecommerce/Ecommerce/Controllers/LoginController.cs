using Ecommerce.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Common.DataMembers.Input;
using Output = Ecommerce.Common.DataMembers.Output;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using System;

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
                    new Claim(ClaimTypes.Email, user.UserName)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

    }
}

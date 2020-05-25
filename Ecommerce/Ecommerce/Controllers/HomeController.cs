using Ecommerce.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Lote = Ecommerce.Common.DataMembers.Output.Lote;


namespace Ecommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly Core.ILoteManager _loteManager;
        private readonly ILogger<HomeController> _logger;
        private readonly Core.IUsuarioManager _usuarioManager;


        public HomeController(Core.ILoteManager loteManager, ILogger<HomeController> logger)
        {
            _loteManager = loteManager;
            _logger = logger;
        }

        [Authorize(Roles = "CLIENT,ADMIN")]//CLIENT - ADMIN
        public IActionResult Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();

                var lote = _loteManager.Get();

                List<Lote> lots = lote.Where(l => l.Activo == true).OrderByDescending(l => l.Id).ToList();

                model = new HomeViewModel
                {
                    Lots = lots,
                };

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }   
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "CLIENT")]
        public IActionResult ShowNotifications()
        {
            var currentUserId = _usuarioManager.GetByName(HttpContext.User.Identity.Name).Id;
            // var notifications = _notificationManager.GetByUser(currentUserId);
            // return View(notifications);
            return View();
        }
    }
}

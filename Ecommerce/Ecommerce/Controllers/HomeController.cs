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
        private readonly Core.INotificacionesManager _notificacionesManager;
        private readonly Core.ILoteManager _loteManager;
        private readonly ILogger<HomeController> _logger;


        public HomeController(Core.ILoteManager loteManager, ILogger<HomeController> logger, Core.INotificacionesManager notificacionesManager)
        {
            _notificacionesManager = notificacionesManager;
            _loteManager = loteManager;
            _logger = logger;
        }

        [Authorize(Roles = "CLIENT,ADMIN")]
        public IActionResult Index()
        {
            try
            {
                HomeViewModel model = new HomeViewModel();

                var lote = _loteManager.Get();

                List<Lote> lots = lote.Where(l => l.Activo == true && l.Estado.Codigo == "ABIERTO").OrderByDescending(l => l.Id).ToList();

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

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactUsViewModel vm)
        {
            try
            {
                if(vm.Email == null || vm.Message == null || vm.Name == null || vm.Subject == null)
                { 
                    ModelState.AddModelError("", "Todos los campos deben ser completados");
                    return View();
                }

                if (ModelState.IsValid)
                {
                   _notificacionesManager.SendRequestConnected(vm.Email,vm.Message,vm.Name,vm.Subject);
                   TempData["SuccesMessage"] = "Mensaje enviado correctamente";
                }

                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

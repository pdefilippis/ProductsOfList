using Ecommerce.Common.DataMembers.Output;
using Ecommerce.ViewModels.Lot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using Input = Ecommerce.Common.DataMembers.Input;

namespace Ecommerce.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Core.IArticuloManager _articuloManager;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(Core.IArticuloManager articuloManager, ILogger<EmployeeController> logger)
        {
            _articuloManager = articuloManager;
            _logger = logger;
        }

        public IActionResult Admin()
        {
            try
            {
                var articulos = _articuloManager.GetByUserInteresado(User.Identity.Name);

                ViewBag.ArticulosAplicados = articulos.Count();
                ViewBag.ArticulosGanados = articulos.Where(x => x.UsuarioAdjudicado?.UserName == User.Identity.Name).Count();
                ViewBag.DineroGastado = articulos.Where(x => x.UsuarioAdjudicado?.UserName == User.Identity.Name).Sum(q => q.Precio);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return RedirectToAction("Status", "Error", new { code = 404 });
            }
            
        }

    }
}

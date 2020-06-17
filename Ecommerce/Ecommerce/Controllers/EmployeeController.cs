using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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

        public JsonResult GetDataEmployee()
        {
            var articulos = _articuloManager.GetByUserInteresado(User.Identity.Name);

            var items = articulos.Select(l => new
            {
                nameLot = l.Lote.Descripcion,
                articleName = l.Descripcion,
                probability = 100 / l.UsuariosInteresados.Count() + "\n%",
                winner = l.Lote.Estado.Codigo.Equals("CERRADO") ? l.UsuarioAdjudicado.UserName == User.Identity.Name ? "v" : "x" : "Sin cerrar"
            }).ToList();

            return Json(items);
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

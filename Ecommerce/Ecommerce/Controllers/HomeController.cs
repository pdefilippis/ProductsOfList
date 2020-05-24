using Ecommerce.Domain;
using Ecommerce.Domain.Models;
using Ecommerce.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Lote = Ecommerce.Common.DataMembers.Output.Lote;


namespace Ecommerce.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly Core.ILoteManager _loteManager;


        public HomeController(Core.ILoteManager loteManager)
        {
            _loteManager = loteManager;
        }

        [Authorize(Roles = "CLIENT,ADMIN")]//CLIENT - ADMIN
        public IActionResult Index()
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


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
    
        //private readonly ProductsManagerContext context;

        //public HomeController(ProductsManagerContext productsManagerContext)
        //{
        //    this.context = productsManagerContext;
        //}


        //public IActionResult Index()
        //{
        //    HomeViewModel model = new HomeViewModel();

        //    List<Lote> lots = context.Lote.Where(x => x.Estado == Lote.EstadoLote.ENABLED).OrderByDescending(x => x.Id).ToList();

        //    model = new HomeViewModel
        //    {
        //        Lots = lots,
        //    };

        //    return View(model);
        //}
        
        public IActionResult Index()
        {
            return View();
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

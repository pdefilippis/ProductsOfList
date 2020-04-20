using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ArticleController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateArticle()
        {
            return View();
        }

        public IActionResult EditArticle()
        {
            return View();
        }

        public IActionResult IndexPublic()
        {
            return View();
        }
    }
}

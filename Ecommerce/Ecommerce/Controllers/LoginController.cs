using Ecommerce.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
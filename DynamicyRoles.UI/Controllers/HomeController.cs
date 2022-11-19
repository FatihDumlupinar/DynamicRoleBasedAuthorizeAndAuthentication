using Microsoft.AspNetCore.Mvc;

namespace DynamicyRoles.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

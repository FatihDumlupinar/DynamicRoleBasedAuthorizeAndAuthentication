using DynamicyRoles.UI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DynamicyRoles.UI.Controllers
{
    [Authorize(Menu = ApplicationService.Enums.AppStaticMenusEnm.User)]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using DynamicyRoles.ApplicationService.Enums;
using DynamicyRoles.UI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DynamicyRoles.UI.Controllers
{
    [Authorize(Menu = AppStaticMenusEnm.Admin)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
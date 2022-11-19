using DynamicyRoles.UI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace DynamicyRoles.UI.Controllers
{
    [Authorize(Menu = ApplicationService.Enums.AppStaticMenusEnm.Role)]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

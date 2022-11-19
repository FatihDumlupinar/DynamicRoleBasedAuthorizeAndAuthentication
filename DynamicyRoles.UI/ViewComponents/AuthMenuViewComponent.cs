using DynamicyRoles.ApplicationService.Enums;
using DynamicyRoles.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DynamicyRoles.UI.ViewComponents
{
    [ViewComponent(Name = "AuthMenu")]
    public class AuthMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (HttpContext.User.Identity.IsAuthenticated==false)
            {
                return View("Default", new List<AuthMenuList>());
            }

            var userAuthMenuList = JsonConvert.DeserializeObject<List<AuthMenuList>>(
                    value: HttpContext.User.FindFirst(ClaimTypesEnm.UserAuth.ToString()).Value
                );

            return View("Default", userAuthMenuList);
        }
    }
}

using DynamicyRoles.ApplicationService.Enums;
using DynamicyRoles.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DynamicyRoles.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AppStaticMenusEnm Menu { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var user = context.HttpContext.User;
            if (user != null)
            {
                if (user.Claims.Any(c => c.Type == ClaimTypesEnm.UserAuth.ToString()))
                {
                    if (Menu == default)//Menu boş gelirse eğer
                    {
                        return;
                    }

                    var userAuthClaim = JsonConvert.DeserializeObject<List<AuthMenuList>>
                        (
                            user.Claims.First(c => c.Type == ClaimTypesEnm.UserAuth.ToString()).Value
                        );

                    if (!userAuthClaim.Any(i => i.MenuId == (int)Menu))
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Errors",
                            action = "UnAuthorized"
                        }));
                    }
                    
                    return;
                }

            }

            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Account",
                action = "Login"
            }));
            return;
        }
    }
}

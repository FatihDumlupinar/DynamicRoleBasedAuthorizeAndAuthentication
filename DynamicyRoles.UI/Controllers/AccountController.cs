using Dapper;
using DynamicyRoles.ApplicationService.Enums;
using DynamicyRoles.ApplicationService.Interfaces;
using DynamicyRoles.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DynamicyRoles.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginModel model)
        {
            var user = _unitOfWork.AppUsers.Get(i => i.Email == model.Email && i.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View(model);
            }

            List<Claim> userClaims = new();
            userClaims.Add(new Claim(ClaimTypesEnm.UserId.ToString(), user.Id.ToString()));

            await using var conn = _unitOfWork.AppDbContext.Database.GetDbConnection();

            var getUserAllAuthData = await conn.QueryAsync<AuthMenuList>(@"
               Select 
                    AppStaticMenus.Id as MenuId,
                    AppStaticMenus.Name,
                    AppStaticMenus.Controller,
                    AppStaticMenus.Action,
                    AppStaticMenus.IsHeader
                from AppRoles, 
                    AppRoleAuthorizes, 
					AppStaticMenus,
                    AppUsersAndAppRoles,
                    AppUsers
                Where AppRoles.Id = AppRoleAuthorizes.AppRoleId and AppRoleAuthorizes.AppStaticMenuId = AppStaticMenus.Id and AppRoles.Id = AppUsersAndAppRoles.AppRoleId and AppUsersAndAppRoles.AppUserId = AppUsers.Id and 
				AppUsers.Id=@UserId
                ", new { UserId = user.Id });

            List<AuthMenuList> authMenuList = new();

            if (getUserAllAuthData.Any())
            {
                AuthMenuList authMenu = new();

                foreach (var auth in getUserAllAuthData)
                {
                    if (!authMenuList.Any(i => i.MenuId == auth.MenuId))
                    {
                        authMenuList.Add(auth);
                    }
                }

            }

            userClaims.Add(new Claim(ClaimTypesEnm.UserAuth.ToString(), JsonConvert.SerializeObject(authMenuList)));

            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.IsRememberMe
            };

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            if (TempData["ReturnUrl"] != null)
            {
                return Redirect(TempData["ReturnUrl"].ToString());
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }


    }
}

using DynamicyRoles.ApplicationService.Enums;
using DynamicyRoles.ApplicationService.Interfaces;
using DynamicyRoles.Domain.Entities;
using DynamicyRoles.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicyRoles.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Index()
        {
            var roles = await _unitOfWork.AppRoles.GetListAsync();

            return View(roles);
        }

        public ActionResult Create()
        {
            RoleCreateModel model = new()
            {
                Authorizes = new()
                {
                    new() { Value = ((int)AppStaticMenusEnm.Admin).ToString(), Text = AppStaticMenusEnm.Admin.ToString() },
                    new() { Value = ((int)AppStaticMenusEnm.User).ToString(), Text = AppStaticMenusEnm.User.ToString() },
                    new() { Value = ((int)AppStaticMenusEnm.Role).ToString(), Text = AppStaticMenusEnm.Role.ToString() },
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleCreateModel model)
        {
            var roleEntity = await _unitOfWork.AppRoles.AddAsyncReturnEntity(new()
            {
                Name = model.Name,
            });

            await _unitOfWork.SaveChangesAsync();

            var roleAuthorizes = model.MenuIds.Select(i => new AppRoleAuthorize()
            {
                AppStaticMenuId = i,
                AppRole = roleEntity

            }).ToList();

            await _unitOfWork.AppRoleAuthorizes.AddAllAsync(roleAuthorizes);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var roleEntity = _unitOfWork.AppRoles.Get(i => i.Id == id);

            var roleAuth = await _unitOfWork.AppRoleAuthorizes.GetListAsync(i => i.AppRole == roleEntity);

            RoleEditModel model = new()
            {
                Name = roleEntity.Name,
                Authorizes = new()
                {
                    new()
                    {
                        Value = ((int)AppStaticMenusEnm.Admin).ToString(),
                        Text = AppStaticMenusEnm.Admin.ToString(),
                        Selected = roleAuth.Any(i => i.AppStaticMenuId == (int)AppStaticMenusEnm.Admin)
                    },
                    new()
                    {
                        Value = ((int)AppStaticMenusEnm.User).ToString(),
                        Text = AppStaticMenusEnm.User.ToString(),
                        Selected = roleAuth.Any(i => i.AppStaticMenuId == (int)AppStaticMenusEnm.User)
                    },
                    new()
                    {
                        Value = ((int)AppStaticMenusEnm.Role).ToString(),
                        Text = AppStaticMenusEnm.Role.ToString(),
                        Selected = roleAuth.Any(i => i.AppStaticMenuId == (int)AppStaticMenusEnm.Role)
                    },
                },
                MenuIds = roleAuth.Select(i => i.AppStaticMenuId).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleEditModel model)
        {
            var roleEntity = _unitOfWork.AppRoles.Get(i => i.Id == model.Id);
            roleEntity.Name = model.Name;
            await _unitOfWork.AppRoles.UpdateAsync(roleEntity);

            var roleAuthEntities = await _unitOfWork.AppRoleAuthorizes.GetListAsync(i => i.AppRole == roleEntity);
            await _unitOfWork.AppRoleAuthorizes.DeleteAllAsync(roleAuthEntities);

            var newRoleAuth = model.MenuIds.Select(i => new AppRoleAuthorize()
            {
                AppStaticMenuId = i,
                AppRole = roleEntity

            }).ToList();
            await _unitOfWork.AppRoleAuthorizes.AddAllAsync(newRoleAuth);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var role = _unitOfWork.AppRoles.Get(i => i.Id == id);
            await _unitOfWork.AppRoles.DeleteAsync(role);

            var roleAuht = await _unitOfWork.AppRoleAuthorizes.GetListAsync(i => i.AppRole == role);
            await _unitOfWork.AppRoleAuthorizes.DeleteAllAsync(roleAuht);

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using DynamicyRoles.ApplicationService.Interfaces;
using DynamicyRoles.Domain.Entities;
using DynamicyRoles.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicyRoles.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Index()
        {
            var users = await _unitOfWork.AppUsers.GetList()
                .Include(x => x.AppUsersAndAppRoles).ThenInclude(x => x.AppRole)
                .AsNoTrackingWithIdentityResolution().ToListAsync();

            var userListModel = users.Select(x => new UserListModel()
            {
                Email = x.Email,
                FullName = x.FullName,
                Id = x.Id,
                Roles = string.Join(",", x.AppUsersAndAppRoles.Select(i => i.AppRole.Name))
            }).ToList();

            return View(userListModel);
        }

        public async Task<ActionResult> Create()
        {
            var roles = await _unitOfWork.AppRoles.GetListAsync();

            UserCreateModel model = new()
            {
                Roles = roles.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserCreateModel model)
        {
            var userEntity = await _unitOfWork.AppUsers.AddAsyncReturnEntity(new()
            {
                Email = model.Email,
                FullName = model.FullName,
                Password = model.Password,

            });

            await _unitOfWork.SaveChangesAsync();

            var roles = await _unitOfWork.AppRoles.GetListAsync(i => model.RoleIds.Contains(i.Id));

            var userAndRoleList = roles.Select(i => new AppUsersAndAppRoles()
            {
                AppRole = i,
                AppUser = userEntity,
            }).ToList();
            await _unitOfWork.AppUsersAndAppRoles.AddAllAsync(userAndRoleList);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = _unitOfWork.AppUsers.Get(i => i.Id == id);

            var appUsersAndAppRoles = await _unitOfWork.AppUsersAndAppRoles.GetListAsync(i=>i.AppUser==user);

            var roles = await _unitOfWork.AppRoles.GetListAsync();

            UserEditModel model = new()
            {
                Id = id,
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password,
                RoleIds = appUsersAndAppRoles.Select(i => i.AppRole.Id).ToList(),
                Roles = roles.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                    Selected = appUsersAndAppRoles.Any(x => x.AppRole == i)

                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditModel model)
        {
            var userEntity = _unitOfWork.AppUsers.Get(i => i.Id == model.Id);

            userEntity.Email = model.Email;
            userEntity.FullName = model.FullName;
            userEntity.Password = model.Password;

            await _unitOfWork.AppUsers.UpdateAsync(userEntity);

            var userAndRoleEntities = await _unitOfWork.AppUsersAndAppRoles.GetListAsync(i => i.AppUser == userEntity);
            await _unitOfWork.AppUsersAndAppRoles.DeleteAllAsync(userAndRoleEntities);

            var roles = await _unitOfWork.AppRoles.GetListAsync(i => model.RoleIds.Contains(i.Id));

            var newUserAndRoleEntities = model.RoleIds.Select(i => new AppUsersAndAppRoles()
            {
                AppRole = roles.First(x => x.Id == i),
                AppUser = userEntity
            }).ToList();
            await _unitOfWork.AppUsersAndAppRoles.AddAllAsync(newUserAndRoleEntities);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            var userEntity = _unitOfWork.AppUsers.Get(i => i.Id == id);
            await _unitOfWork.AppUsers.DeleteAsync(userEntity);

            var userAndRoleEntities = await _unitOfWork.AppUsersAndAppRoles.GetListAsync(i => i.AppUser == userEntity);
            await _unitOfWork.AppUsersAndAppRoles.DeleteAllAsync(userAndRoleEntities);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

using DynamicyRoles.ApplicationService.Enums;
using DynamicyRoles.Domain.Entities;
using DynamicyRoles.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicyRoles.Persistence.Seed
{
    public static class SeedData
    {
        public static async Task SeedAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            await dbContext.Database.MigrateAsync();

            if (!await dbContext.AppRoles.AnyAsync()&&!await dbContext.AppUsers.AnyAsync())
            {
                var staticMenus = new List<AppStaticMenu>
                {
                    new(){
                        Id=(int)AppStaticMenusEnm.Admin,
                        Action="Index",
                        Controller="Admin",
                        IsHeader=true,
                        Name=AppStaticMenusEnm.Admin.ToString()
                    },
                    new(){
                        Id=(int)AppStaticMenusEnm.User,
                        Action="Index",
                        Controller="User",
                        IsHeader=true,
                        Name=AppStaticMenusEnm.User.ToString()
                    },
                    new(){
                        Id=(int)AppStaticMenusEnm.Role,
                        Action="Index",
                        Controller="Role",
                        IsHeader=true,
                        Name=AppStaticMenusEnm.Role.ToString()
                    },
                };

                await dbContext.AppStaticMenus.AddRangeAsync(staticMenus);

                var roles = new List<AppRole>()
                {
                    new(){Name="Admin" },
                    new(){Name="User" }
                };

                await dbContext.AppRoles.AddRangeAsync(roles);
                await dbContext.SaveChangesAsync();

                var rolesAuthorizes = new List<AppRoleAuthorize>()
                {
                    new(){AppRole=roles[0],AppStaticMenu=staticMenus[0]},
                    new(){AppRole=roles[0],AppStaticMenu=staticMenus[1]},
                    new(){AppRole=roles[0],AppStaticMenu=staticMenus[2]},
                    new(){AppRole=roles[1],AppStaticMenu=staticMenus[1]},
                };

                await dbContext.AppRoleAuthorizes.AddRangeAsync(rolesAuthorizes);

                var users = new List<AppUser>()
                {
                    new(){Email="admin@email.com",Password="123456",FullName="Admin User"},
                    new(){Email="user@email.com",Password="123456",FullName="Standart User"},
                };

                await dbContext.AppUsers.AddRangeAsync(users);
                await dbContext.SaveChangesAsync();

                await dbContext.AppUsersAndAppRoles.AddRangeAsync(new List<AppUsersAndAppRoles>() 
                {
                    new(){AppRole=roles[0],AppUser=users[0] },
                    new(){AppRole=roles[1],AppUser=users[1] },

                });
                await dbContext.SaveChangesAsync();
            }

        }
    }
}

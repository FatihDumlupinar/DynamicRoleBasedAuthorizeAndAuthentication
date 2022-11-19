using DynamicyRoles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicyRoles.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppRoleAuthorize> AppRoleAuthorizes { get; set; }
        public DbSet<AppStaticMenu> AppStaticMenus { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUsersAndAppRoles> AppUsersAndAppRoles { get; set; }

    }
}

using DynamicyRoles.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicyRoles.ApplicationService.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AppRole> AppRoles { get; }
        IRepository<AppRoleAuthorize> AppRoleAuthorizes { get; }
        IRepository<AppUser> AppUsers { get; }

        DbContext AppDbContext { get; }

        Task<bool> SaveChangesAsync();
    }
}

using DynamicyRoles.ApplicationService.Interfaces;
using DynamicyRoles.Domain.Entities;
using DynamicyRoles.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DynamicyRoles.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Ctor&Fields
        
        private bool disposedValue = false;

        protected readonly AppDbContext _appDbContext;

        private readonly IRepository<AppRole> _appRoleRepository;
        private readonly IRepository<AppRoleAuthorize> _appRoleAuthorizeRepository;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppUsersAndAppRoles> _appUsersAndAppRolesRepository;

        public UnitOfWork(AppDbContext appDbContext, IRepository<AppRole> appRoleRepository, IRepository<AppRoleAuthorize> appRoleAuthorizeRepository, IRepository<AppUser> appUserRepository, IRepository<AppUsersAndAppRoles> appUsersAndAppRolesRepository)
        {
            this.disposedValue = disposedValue;
            _appDbContext = appDbContext;
            _appRoleRepository = appRoleRepository;
            _appRoleAuthorizeRepository = appRoleAuthorizeRepository;
            _appUserRepository = appUserRepository;
            _appUsersAndAppRolesRepository = appUsersAndAppRolesRepository;
        }

        #endregion

        #region Properties
        
        public IRepository<AppRole> AppRoles => _appRoleRepository;

        public IRepository<AppRoleAuthorize> AppRoleAuthorizes => _appRoleAuthorizeRepository;

        public IRepository<AppUser> AppUsers => _appUserRepository;

        public IRepository<AppUsersAndAppRoles> AppUsersAndAppRoles => _appUsersAndAppRolesRepository; 

        public DbContext AppDbContext => _appDbContext;

        #endregion

        #region Methods
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveChangesAsync()
        {
            await using var dbContextTransaction = _appDbContext.Database.BeginTransaction();
            try
            {
                await _appDbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();
                return false;
            }
        } 

        #endregion
    }
}

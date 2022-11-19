using DynamicyRoles.Domain.Common;

namespace DynamicyRoles.Domain.Entities
{
    public class AppUsersAndAppRoles : BaseEntity
    {
        public virtual AppRole AppRole { get; set; }

        public virtual AppUser AppUser { get; set; }

    }
}

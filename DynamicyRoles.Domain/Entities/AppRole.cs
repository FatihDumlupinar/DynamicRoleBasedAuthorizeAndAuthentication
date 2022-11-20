using DynamicyRoles.Domain.Common;

namespace DynamicyRoles.Domain.Entities
{
    public class AppRole : BaseEntity
    {
        public string Name { get; set; }
        public virtual IList<AppUsersAndAppRoles> AppUsersAndAppRoles { get; set; }
    }
}

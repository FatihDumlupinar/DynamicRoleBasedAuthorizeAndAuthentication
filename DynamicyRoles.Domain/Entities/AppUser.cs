using DynamicyRoles.Domain.Common;

namespace DynamicyRoles.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string FullName { get; set; } = "";
        
        public string Email { get; set; } = "";

        public string Password { get; set; } = "";

        public virtual IList<AppUsersAndAppRoles> AppUsersAndAppRoles { get; set; }

    }
}

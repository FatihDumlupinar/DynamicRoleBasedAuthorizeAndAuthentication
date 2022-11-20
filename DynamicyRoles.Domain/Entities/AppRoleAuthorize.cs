using DynamicyRoles.Domain.Common;

namespace DynamicyRoles.Domain.Entities
{
    public class AppRoleAuthorize : BaseEntity
    {
        public AppRole AppRole { get; set; }

        public int AppStaticMenuId { get; set; }

    }
}

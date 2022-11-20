using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicyRoles.UI.Models
{
    public class RoleEditModel
    {
        public string Name { get; set; } = "";
        public int Id { get; set; }
        public List<SelectListItem> Authorizes { get; set; } = new();
        public List<int> MenuIds { get; set; } = new();

    }
}

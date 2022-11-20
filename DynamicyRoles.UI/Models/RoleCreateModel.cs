using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicyRoles.UI.Models
{
    public class RoleCreateModel
    {
        public string Name { get; set; } = "";
        
        public List<int> MenuIds { get; set; } = new();

        public List<SelectListItem> Authorizes { get; set; } = new();

    }
   
}

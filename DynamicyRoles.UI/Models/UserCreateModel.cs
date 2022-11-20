using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicyRoles.UI.Models
{
    public class UserCreateModel
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public List<int> RoleIds { get; set; } = new();

        public List<SelectListItem> Roles { get; set; } = new();


    }
}

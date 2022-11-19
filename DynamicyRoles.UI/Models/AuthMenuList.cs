namespace DynamicyRoles.UI.Models
{
    public class AuthMenuList
    {
        public int MenuId { get; set; }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool IsHeader { get; set; }
    }
}

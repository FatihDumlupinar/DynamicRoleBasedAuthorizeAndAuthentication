namespace DynamicyRoles.UI.Models
{
    public class UserListModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Roles { get; set; } = "";
    }
}

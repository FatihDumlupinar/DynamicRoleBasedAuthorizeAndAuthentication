using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicyRoles.Domain.Entities
{
    public class AppStaticMenu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public bool IsHeader { get; set; }

    }
}

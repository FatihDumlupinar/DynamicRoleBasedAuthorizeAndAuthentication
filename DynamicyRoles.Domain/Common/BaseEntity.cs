using DynamicyRoles.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DynamicyRoles.Domain.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

    }
}

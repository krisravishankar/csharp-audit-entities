using System.ComponentModel.DataAnnotations;

namespace AuditableEntities.Web.Models
{
    public class Book : BaseEntity<int>, IEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public int Pages { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AuditableEntities.Web.DTOs
{
    public class BookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public int Pages { get; set; }
    }
}

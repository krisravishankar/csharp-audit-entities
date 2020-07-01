using System;
using System.ComponentModel.DataAnnotations;

namespace AuditableEntities.Web.Models
{
    public class BaseEntity<T> : IEntity
    {
        [Required]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { Id = (T)Convert.ChangeType(value, typeof(T)); }
        }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOnUtc { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOnUtc { get; set; }

        public string IPAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}

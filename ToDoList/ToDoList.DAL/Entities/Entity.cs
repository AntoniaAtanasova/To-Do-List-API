using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.DAL.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            CreatedAt = DateTime.Now;
            LastModifiedAt = DateTime.Now;
        }

        public int Id { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User Creator { get; set; }

        [ForeignKey("LastModifiedBy")]
        public virtual User LastModifier { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedAt { get; }

        public DateTime LastModifiedAt { get; set; }
    }
}

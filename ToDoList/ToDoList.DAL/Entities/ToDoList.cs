using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DAL.Entities
{
    public class ToDoList : Entity
    {
        public ToDoList()
        {
            Tasks = new HashSet<Task>();
            Users = new HashSet<User>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

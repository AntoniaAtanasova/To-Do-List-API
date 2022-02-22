using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DAL.Entities
{
    public class Task
    {
        public Task()
        {
            Users = new HashSet<User>();
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int ToDoListId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsComplete { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

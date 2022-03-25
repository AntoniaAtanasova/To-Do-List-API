using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DAL.Entities
{
    public class Task : Entity
    {
        public Task()
        {
            Users = new HashSet<User>();
            IsComplete = false;
        }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public int ToDoListId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsComplete { get; set; }

        public DateTime Date { get; set; }

        public DayType Day { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}

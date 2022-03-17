using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
namespace ToDoList.DAL.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            ToDoLists = new HashSet<ToDoList>();
            Tasks = new HashSet<Task>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedAt { get; private set; }

        public virtual ICollection<ToDoList> ToDoLists { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}

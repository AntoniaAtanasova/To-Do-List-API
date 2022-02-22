using Microsoft.AspNet.Identity.EntityFramework;
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

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ToDoList> ToDoLists { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}

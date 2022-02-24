using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Configurations;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Entities.ToDoList> ToDoLists { get; set; }
        public DbSet<SharedToDoList> SharedToDoLists { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new ToDoListConfiguration());
        }
    }  
}

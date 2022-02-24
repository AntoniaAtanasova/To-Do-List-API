using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                .HasMany(u => u.ToDoLists)
                .WithMany(t => t.Users)
                .UsingEntity<SharedToDoList>
                            (sl => sl.HasOne<Entities.ToDoList>().WithMany(),
                            sl => sl.HasOne<User>().WithMany());

            user
                .HasMany(t => t.Tasks)
                .WithMany(u => u.Users)
                .UsingEntity<AssignedTask>
                           (at => at.HasOne<Task>().WithMany(),
                            at => at.HasOne<User>().WithMany());

        }
    }
}

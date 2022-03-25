using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> task)
        {
            task
                .HasOne(u => u.Creator)
                         .WithMany()
                         .OnDelete(DeleteBehavior.ClientCascade);

            task
                .HasOne(u => u.LastModifier)
                         .WithMany()
                         .HasForeignKey(u => u.LastModifiedBy)
                         .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}

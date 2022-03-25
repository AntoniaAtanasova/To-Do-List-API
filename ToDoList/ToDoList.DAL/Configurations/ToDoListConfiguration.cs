using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DAL.Configurations
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<Entities.ToDoList>
    {
        public void Configure(EntityTypeBuilder<Entities.ToDoList> toDolist)
        {
            toDolist
               .HasOne(u => u.Creator)
                    .WithMany()
                    .OnDelete(DeleteBehavior.ClientCascade);

            toDolist
                .HasOne(u => u.LastModifier)
                     .WithMany()
                     .HasForeignKey(u => u.LastModifiedBy)
                     .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}

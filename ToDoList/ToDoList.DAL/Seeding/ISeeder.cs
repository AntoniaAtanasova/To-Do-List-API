using System;
using System.Threading.Tasks;

namespace ToDoList.DAL.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(DatabaseContext context, IServiceProvider serviceProvider);
    }
}

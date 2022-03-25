using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;


namespace ToDoList.DAL.Seeding
{
    public class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                DatabaseContext context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                IHttpClientFactory clientFactory = serviceScope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

                if (context.Database.EnsureCreated())
                {
                    var seeders = new List<ISeeder>
                          {
                              new AdminSeeder(),
                              new HolidaysSeeder(clientFactory)
                          };

                    foreach (var seeder in seeders)
                    {
                        await seeder.SeedAsync(context, applicationServices);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.Common;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Seeding
{
    public class HolidaysSeeder : ISeeder
    {
        private readonly HttpClient client;

        public HolidaysSeeder(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient(Constants.HolidayApiClientName);
        }

        public async System.Threading.Tasks.Task SeedAsync(DatabaseContext context, IServiceProvider serviceProvider)
        {
            var currentYear = DateTime.Now.Year;
            var holidays = await AddFromApi(Constants.BulgariaCode, currentYear);

            foreach (var holiday in holidays)
            {
                await context.Holidays.AddAsync(holiday);
            }
        }

        private async Task<List<Holiday>> AddFromApi(string countryCode, int year)
        {
            var url = string.Format(Constants.HolidayApiUrl, year, countryCode);
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Holiday>>(stringResponse,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }

            throw new HttpRequestException(response.ReasonPhrase);
        }
    }
}

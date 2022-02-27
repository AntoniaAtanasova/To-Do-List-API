using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly DatabaseContext _databaseContext;

        public HolidayRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Holiday>> GetAll()
        {
            return await _databaseContext.Holidays.ToListAsync();
        }
    }
}

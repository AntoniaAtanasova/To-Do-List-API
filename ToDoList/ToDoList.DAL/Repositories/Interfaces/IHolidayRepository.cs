using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Repositories.Interfaces
{
    public interface IHolidayRepository
    {
        public Task<List<Holiday>> GetAll();
    }
}

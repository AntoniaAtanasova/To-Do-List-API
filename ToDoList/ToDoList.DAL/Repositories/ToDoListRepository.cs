using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {

        private readonly DatabaseContext _databaseContext;

        public ToDoListRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> Create(Entities.ToDoList list)
        {
            await _databaseContext.ToDoLists.AddAsync(list);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var list = await GetById(id);

            _databaseContext.ToDoLists.Remove(list);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(int listId, Entities.ToDoList newList, string userId)
        {
            var list = await GetById(listId);

            list.Title = newList.Title;
            list.LastModifiedBy = userId;

            _databaseContext.Update(list);
            await _databaseContext.SaveChangesAsync();

            return true;

        }

        public Task<List<Entities.ToDoList>> GetAll()
        {
            return _databaseContext.ToDoLists.ToListAsync();
        }

        public async Task<Entities.ToDoList> GetById(int id)
        {
            return await _databaseContext.ToDoLists.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Entities.ToDoList>> GetMy(User loggedIn)
        {
            return await _databaseContext.ToDoLists.Where(l => l.Users.Contains(loggedIn)).ToListAsync();
        }

        public async Task<bool> IsListNameTaken(string title, User loggedin)
        {
            var lists = await GetMy(loggedin);

            return lists.Any(l => l.Title == title);
        }

        public async Task<bool> Share(int listId, string userId)
        {
            await _databaseContext.SharedToDoLists.AddAsync(new SharedToDoList()
            {
                UserId = userId,
                ToDoListId = listId
            });
            await _databaseContext.SaveChangesAsync();
            
            return true;
        }
    }
}

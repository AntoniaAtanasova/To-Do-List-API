using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> Delete(Entities.ToDoList list)
        {
            _databaseContext.ToDoLists.Remove(list);
            await _databaseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(Entities.ToDoList listToEdit, Entities.ToDoList newList, string userId)
        {
            listToEdit.Title = newList.Title;
            listToEdit.LastModifiedBy = userId;
            listToEdit.LastModifiedAt = DateTime.Now;

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

        public async Task<bool> IsSharedWithUser(int listId, string userId)
        {
            return await _databaseContext.SharedToDoLists.AnyAsync(tl => tl.UserId == userId && tl.ToDoListId == listId);
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

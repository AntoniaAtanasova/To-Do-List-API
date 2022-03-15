using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Repositories.Interfaces
{
    public interface IToDoListRepository
    {
        public Task<bool> Create(Entities.ToDoList list);

        public Task<bool> Edit(Entities.ToDoList listToEdit, Entities.ToDoList newList, string userId);

        public Task<bool> Delete(Entities.ToDoList list);

        public Task<Entities.ToDoList> GetById(int id);

        public Task<List<Entities.ToDoList>> GetAll();

        public Task<List<Entities.ToDoList>> GetMy(User loggedIn);

        public Task<bool> Share(int listId, string userId);

        public Task<bool> IsSharedWithUser(int listId, string userId);

        public Task<bool> IsListNameTaken(string title, User loggedin);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Interfaces;
using ToDoList.Common;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Repositories.Interfaces;

namespace ToDoList.BLL.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly IToDoListRepository _repo;

        public ToDoListService(IToDoListRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Create(DAL.Entities.ToDoList toDoList, User user)
        {
            if(!await _repo.IsListNameTaken(toDoList.Title, user))
            {
                toDoList.CreatedBy = user.Id;
                toDoList.LastModifiedBy = user.Id;

                return await _repo.Create(toDoList);
            }

            throw new ToDoListException(toDoList.Title, Constants.ListTitleTaken, Constants.BadRequest);
        }

        public async Task<bool> Delete(int id)
        {
            var list = await _repo.GetById(id);

            if (list == null)
            {
                throw new ToDoListException(Constants.ListNotFound, Constants.NotFound);
            }

            return await _repo.Delete(list);
        }

        public async Task<bool> Edit(int id, DAL.Entities.ToDoList newList, string userId)
        {
            var list = await _repo.GetById(id);

            if (list == null)
            {
                throw new ToDoListException(Constants.ListNotFound, Constants.NotFound);
            }

            return await _repo.Edit(list, newList, userId);
        }

        public async Task<List<DAL.Entities.ToDoList>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<DAL.Entities.ToDoList> GetById(int id)
        {
            var list = await _repo.GetById(id);

            if (list == null)
            {
                throw new ToDoListException(Constants.ListNotFound, Constants.NotFound);
            }

            return list;
        }

        public Task<List<DAL.Entities.ToDoList>> GetMy(User user)
        {
            return _repo.GetMy(user);
        }

        public Task<bool> Share(int listId, string userId)
        {
            return _repo.Share(listId, userId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetById(string id);

        public Task<List<User>> GetAll();

        public Task<User> GetCurrentUser(ClaimsPrincipal principal);

        public Task<bool> IsUserInRole(User user, string role);

        public System.Threading.Tasks.Task Create(User userToCreate, string password, string role);

        public System.Threading.Tasks.Task Edit(string userId, User newUserData, string role);

        public System.Threading.Tasks.Task Delete(string userId);
    }
}

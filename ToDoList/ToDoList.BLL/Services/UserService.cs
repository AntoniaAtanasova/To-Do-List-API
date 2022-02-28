using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;

        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public System.Threading.Tasks.Task Create(User userToCreate, string password, string role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task Delete(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserInRole(User user, string role)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task Update(string userId, User newUserData, string role)
        {
            throw new NotImplementedException();
        }
    }
}

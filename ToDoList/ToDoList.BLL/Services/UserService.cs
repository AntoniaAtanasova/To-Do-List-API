using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Interfaces;
using ToDoList.Common;
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

        public async System.Threading.Tasks.Task Create(User userToCreate, string password, string role)
        {
            if (role != "Admin" && role != "User")
            {
                throw new ToDoListException(Constants.UserRoleNotValid, Constants.BadRequest);
            }

            if (await _userManager.FindByNameAsync(userToCreate.UserName) != null)
            {
                throw new ToDoListException(userToCreate.UserName, Constants.UserNameNotAvailable, Constants.BadRequest);
            }

            if ((await _userManager.FindByEmailAsync(userToCreate.Email)) != null)
            {
                throw new ToDoListException(Constants.EmailAlreadyInUse, Constants.BadRequest);
            }

            User user = new User()
            {
                UserName = userToCreate.UserName,
                Email = userToCreate.Email,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
            };

            var result = await _userManager.CreateAsync(user, password);

            var error = result.Errors.FirstOrDefault();

            if (error != null)
            {
                throw new ToDoListException(error.Description);
            }

            await _userManager.AddToRoleAsync(user, role);
        }

        public async System.Threading.Tasks.Task Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ToDoListException(Constants.UserNotFound, Constants.NotFound);
            }

            await _userManager.DeleteAsync(user);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userManager.GetAllUsersAsync();
        }

        public async Task<User> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new ToDoListException(Constants.UserNotFound, Constants.NotFound);
            }

            return user;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<bool> IsUserInRole(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async System.Threading.Tasks.Task Edit(string userId, User newUserData, string role)
        {
            User user = await GetById(userId);

            user.UserName = newUserData.UserName;
            user.Email = newUserData.Email;
            user.FirstName = newUserData.FirstName;
            user.LastName = newUserData.LastName;

            var result = await _userManager.UpdateAsync(user);

            var error = result.Errors.FirstOrDefault();

            if (error != null)
            {
                throw new ToDoListException(error.Description);
            }

            if (!await _userManager.IsInRoleAsync(user, role))
            {
                string roleToRemove = role == "Admin" ? "User" : "Admin";

                await _userManager.RemoveRoleFromUser(user, roleToRemove);
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}

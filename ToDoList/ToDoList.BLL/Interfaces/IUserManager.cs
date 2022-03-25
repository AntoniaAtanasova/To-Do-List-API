using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;

namespace ToDoList.BLL.Interfaces
{
    public interface IUserManager
    {
        public Task<IdentityResult> CreateAsync(User user, string password);

        public Task<IdentityResult> AddToRoleAsync(User user, string role);

        public Task<IdentityResult> UpdateAsync(User user);

        public Task<User> FindByIdAsync(string id);

        public Task<User> FindByNameAsync(string name);

        public Task<IdentityResult> DeleteAsync(User user);

        public Task<bool> IsInRoleAsync(User user, string role);

        public Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        public Task<List<User>> GetAllUsersAsync();

        public Task<List<string>> GetUserRolesAsync(User user);

        public Task<bool> ValidateUserCredentials(string userName, string password);

        public System.Threading.Tasks.Task RemoveRoleFromUser(User user, string role);

        public Task<User> FindByEmailAsync(string email);
    }
}


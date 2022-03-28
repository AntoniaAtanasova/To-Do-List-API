using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Web.Authorization.Requirements
{
    public class AdminOrTaskCreatorOrAssignedToRequirement : IAuthorizationRequirement
    {
        public AdminOrTaskCreatorOrAssignedToRequirement() { }
    }
}

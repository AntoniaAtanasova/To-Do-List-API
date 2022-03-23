using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Web.Authorization.Requirements
{
    public class AdminOrListCreatorRequirement : IAuthorizationRequirement
    {
        public AdminOrListCreatorRequirement() { }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using ToDoList.BLL.Exceptions;
using ToDoList.BLL.Interfaces;
using ToDoList.Common;
using ToDoList.Web.Authorization.Requirements;

namespace ToDoList.Web.Authorization.Handlers
{
    public class AdminOrTaskCreatorOrAssignedToHandler: AuthorizationHandler<AdminOrTaskCreatorOrAssignedToRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public AdminOrTaskCreatorOrAssignedToHandler(IUserService userService, ITaskService taskService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _taskService = taskService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrTaskCreatorOrAssignedToRequirement requirement)
        {
            var loggedUser = await _userService.GetCurrentUser(context.User);
            bool isAdmin = await _userService.IsUserInRole(loggedUser, "Admin");

            string taskId = _httpContextAccessor.HttpContext.GetRouteValue("taskId")?.ToString();

            if (isAdmin)
            {
                context.Succeed(requirement);
            }
            else if (taskId != null)
            {
                var task = await _taskService.GetById(int.Parse(taskId));
                var tasks = await _taskService.GetAllMy(loggedUser);

                if (tasks.Contains(task))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                    throw new ToDoListException(task.Id, Constants.NotTaskCreatorOrAssignedTo, Constants.BadRequest);
                }
            }
            else
            {
                context.Fail();
                throw new ToDoListException(Constants.TaskNotFound, Constants.NotFound);
            }
        }
    }
}

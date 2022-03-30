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
    public class AdminOrListCreatorHandler: AuthorizationHandler<AdminOrListCreatorRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IToDoListService _toDoListService;
        private readonly ITaskService _taskService;

        public AdminOrListCreatorHandler(IUserService userService, IToDoListService toDoListService, ITaskService taskService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _toDoListService = toDoListService;
            _taskService = taskService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrListCreatorRequirement requirement)
        {
            var loggedUser = await _userService.GetCurrentUser(context.User);
            bool isAdmin = await _userService.IsUserInRole(loggedUser, "Admin");

            string listId = _httpContextAccessor.HttpContext.GetRouteValue("listId")?.ToString();
            string taskId = _httpContextAccessor.HttpContext.GetRouteValue("taskId")?.ToString();

            if (isAdmin)
            {
                context.Succeed(requirement);
            }
            else if (listId != null && taskId == null)
            {
                var list = await _toDoListService.GetById(int.Parse(listId));

                if(list.CreatedBy == loggedUser.Id)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                    throw new ToDoListException(list.Id, Constants.NotCreator, Constants.BadRequest);
                }
            }
            else if(listId == null && taskId != null)
            {
                var task = await _taskService.GetById(int.Parse(taskId));
                var list = await _toDoListService.GetById(task.ToDoListId);

                if (list.CreatedBy == loggedUser.Id)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                    throw new ToDoListException(list.Id, Constants.NotCreator, Constants.BadRequest);
                }
            }
            else
            {
                context.Fail();
                throw new ToDoListException(Constants.ListNotFound, Constants.NotFound);
            }
        }
    }
}

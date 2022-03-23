using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.Web.DTOs.Requests;
using ToDoList.Web.DTOs.Responses;

namespace ToDoList.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(ITaskService taskService, IUserService userService, IMapper mapper)
        {
            _taskService = taskService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDTO>> Get(int id)
        {
            var task = await _taskService.GetById(id);

            return _mapper.Map<TaskResponseDTO>(task);
        }

        [HttpGet]
        [Route("AllForList/{listId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<IEnumerable<TaskResponseDTO>> GetAllForList(int listId)
        {
            var tasks = await _taskService.GetAllForList(listId);

            return _mapper.Map<IEnumerable<TaskResponseDTO>>(tasks);
        }

        [HttpGet]
        [Route("My")]
        public async Task<IEnumerable<TaskResponseDTO>> GetAllMy()
        {
            User loggedInUser = await _userService.GetCurrentUser(User);

            var tasks = await _taskService.GetAllMy(loggedInUser);

            return _mapper.Map<IEnumerable<TaskResponseDTO>>(tasks);
        }

        [HttpPost]
        [Route("{listId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Post(int listId, TaskRequestDTO task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User loggedInUser = await _userService.GetCurrentUser(User);

            await _taskService.Create(listId, _mapper.Map<TaskRequestDTO, DAL.Entities.Task>(task), loggedInUser);

            return Ok();
        }

        [HttpPost]
        [Route("Assign/{taskId}/{userId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Assign(int taskId, string userId)
        {
            await _taskService.Assign(taskId, userId);

            return Ok();
        }

        [HttpPatch]
        [Route("Complete/{taskId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Complete(int taskId)
        {
            await _taskService.Complete(taskId);

            return Ok();
        }

        [HttpPut("{taskId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Edit(int taskId, TaskRequestDTO task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User loggedInUser = await _userService.GetCurrentUser(User);

            await _taskService.Edit(taskId, _mapper.Map<TaskRequestDTO, DAL.Entities.Task>(task), loggedInUser.Id);

            return Ok();
        }

        [HttpDelete("{taskId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Delete(int taskId)
        {
            await _taskService.Delete(taskId);

            return Ok();
        }
    }
}

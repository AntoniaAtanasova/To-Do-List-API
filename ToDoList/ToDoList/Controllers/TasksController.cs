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

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDTO>> Get(int id)
        {
            var task = await _taskService.GetById(id);

            return _mapper.Map<TaskResponseDTO>(task);
        }

        [HttpGet]
        [Route("listId")]
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
        public async Task<ActionResult> Post(TaskRequestDTO task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User loggedInUser = await _userService.GetCurrentUser(User);

            await _taskService.Create(_mapper.Map<TaskRequestDTO, DAL.Entities.Task>(task), loggedInUser);

            return Ok();
        }

        [HttpPost]
        [Route("Assign/{taskId}/{userId}")]
        public async Task<ActionResult> Assign(int taskId, string userId)
        {
            await _taskService.Assign(taskId, userId);

            return Ok();
        }

        [HttpPatch]
        [Route("Complete/{id}")]
        public async Task<ActionResult> Complete(int id)
        {
            await _taskService.Complete(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, TaskRequestDTO task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User loggedInUser = await _userService.GetCurrentUser(User);

            await _taskService.Edit(id, _mapper.Map<TaskRequestDTO, DAL.Entities.Task>(task), loggedInUser.Id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _taskService.Delete(id);

            return Ok();
        }
    }
}

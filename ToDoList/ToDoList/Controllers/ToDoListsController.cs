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
    public class ToDoListsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IToDoListService _toDoListService;
        private readonly IMapper _mapper;

        public ToDoListsController(IToDoListService toDoListService, IUserService userService, IMapper mapper)
        {
            _toDoListService = toDoListService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ListResponseDTO>> Get(int id)
        {
            var list = await _toDoListService.GetById(id);

            return _mapper.Map<ListResponseDTO>(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<ListResponseDTO>> Get()
        {
            var lists = await _toDoListService.GetAll();

            return _mapper.Map<IEnumerable<ListResponseDTO>>(lists);
        }

        [HttpGet]
        [Route("My")]
        public async Task<IEnumerable<ListResponseDTO>> GetMy()
        {
            User loggedInUser = await _userService.GetCurrentUser(User);

            var lists = await _toDoListService.GetMy(loggedInUser);

            return _mapper.Map<IEnumerable<ListResponseDTO>>(lists);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ListRequestDTO list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User loggedInUser = await _userService.GetCurrentUser(User);

            await _toDoListService.Create(_mapper.Map<ListRequestDTO, DAL.Entities.ToDoList>(list), loggedInUser);

            return Ok();
        }

        [HttpPost]
        [Route("Share/{listId}/{userId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Share(int listId, string userId)
        {
            await _toDoListService.Share(listId, userId);

            return Ok();
        }

        [HttpPut("{listId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Edit(int listId, ListRequestDTO list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User loggedInUser = await _userService.GetCurrentUser(User);

            await _toDoListService.Edit(listId, _mapper.Map<ListRequestDTO, DAL.Entities.ToDoList>(list), loggedInUser.Id);

            return Ok();
        }

        [HttpDelete("{listId}")]
        [Authorize(Policy = "ListCreator")]
        public async Task<ActionResult> Delete(int listId)
        {
            await _toDoListService.Delete(listId);

            return Ok();
        }
    }
}

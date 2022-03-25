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
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Get(string id)
        {
            var userEntity = await _userService.GetById(id);

            return _mapper.Map<UserResponseDTO>(userEntity);
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponseDTO>> Get()
        {
            var users = await _userService.GetAll();

            return _mapper.Map<ICollection<UserResponseDTO>>(users);
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserRequestDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.Create(_mapper.Map<UserRequestDTO, User>(user), user.Password, user.Role);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(string id, UserRequestDTO newUserData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.Edit(id, _mapper.Map<UserRequestDTO, User>(newUserData), newUserData.Role);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.Delete(id);

            return Ok();
        }
    }
}

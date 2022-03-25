using AutoMapper;
using ToDoList.DAL.Entities;
using ToDoList.Web.DTOs.Requests;
using ToDoList.Web.DTOs.Responses;

namespace ToDoList.Web.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskRequestDTO, Task>();
            CreateMap<Task, TaskResponseDTO>();
        }
    }
}

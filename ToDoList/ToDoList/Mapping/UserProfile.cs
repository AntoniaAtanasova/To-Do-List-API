
using AutoMapper;
using ToDoList.DAL.Entities;
using ToDoList.Web.DTOs.Requests;
using ToDoList.Web.DTOs.Responses;

namespace ToDoList.Web.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestDTO, User>();
            CreateMap<User, UserResponseDTO>();
        }
    }
}

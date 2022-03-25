using AutoMapper;
using ToDoList.Web.DTOs.Requests;
using ToDoList.Web.DTOs.Responses;

namespace ToDoList.Web.Mapping
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<ListRequestDTO, DAL.Entities.ToDoList>();
            CreateMap<DAL.Entities.ToDoList, ListResponseDTO>();
        }
    }
}

using System;
using ToDoList.DAL.Entities;

namespace ToDoList.Web.DTOs.Responses
{
    public class TaskResponseDTO : BaseResponseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsComplete { get; set; }

        public DateTime Date { get; set; }

        public string Day { get; set; }
    }
}

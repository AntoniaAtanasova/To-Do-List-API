using System;

namespace ToDoList.Web.DTOs.Responses
{
    public class BaseResponseDTO
    {
        public int Id { set; get; }

        public string CreatedBy { set; get; }

        public string LastModifiedBy { set; get; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModifiedAt { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Web.DTOs.Requests
{
    public class TaskRequestDTO
    {
        [Required]
        public int ToDoListId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

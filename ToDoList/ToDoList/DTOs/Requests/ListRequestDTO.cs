using System.ComponentModel.DataAnnotations;

namespace ToDoList.Web.DTOs.Requests
{
    public class ListRequestDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}

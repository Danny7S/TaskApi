using System.ComponentModel.DataAnnotations;

namespace TaskApi.DTO
{
    public class TaskEntityDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime ModifiedAt { get; set; }
    }

    public class CreateTaskDTO
    {

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(30, ErrorMessage = "Title cannot be longer than 30 characters!")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters!")]
        public string Description { get; set; } = string.Empty;


        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public bool IsCompleted { get; set; } = false;
    }
    public class UpdateTaskDTO
    {
        [Required(ErrorMessage ="Id is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}

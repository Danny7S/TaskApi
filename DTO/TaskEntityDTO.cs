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

        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime ModifiedAt { get; set; }
    }
    public class UpdateTaskDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public bool IsCompleted { get; set; } = false;

        public DateTime ModifiedAt { get; set; }
    }
}

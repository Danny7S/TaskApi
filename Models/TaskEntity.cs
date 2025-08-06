namespace TaskApi.Models;
using System.ComponentModel.DataAnnotations;

public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    [Required]
    public bool IsCompleted { get; set; } = false;

    public DateTime ModifiedAt { get; set; }
}
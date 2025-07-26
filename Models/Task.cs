namespace TaskApi.Models;
using System.ComponentModel.DataAnnotations;

public class Task
{
    [Key]
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool Approved { get; set; }
}
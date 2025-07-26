using Microsoft.EntityFrameworkCore;

namespace TaskApi
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext>options) : base(options)
        {
            
        }


        public DbSet<TaskApi.Models.Task> Tasks { get; set; }
    }
}

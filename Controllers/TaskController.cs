using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;
namespace TaskApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Models.Task>>> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<List<Models.Task>>> AddTask(Models.Task toDotask)
        {
            _context.Tasks.Add(toDotask);
            await _context.SaveChangesAsync();


            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<Models.Task>>> UpdateTask(Models.Task toDotask)
        {
            var dBTask = await _context.Tasks.FindAsync(toDotask.TaskId);
            if (dBTask == null)
            {
                return NotFound("Task not found");
            }
            dBTask.Title = toDotask.Title;
            dBTask.Description = toDotask.Description;
            dBTask.IsCompleted = toDotask.IsCompleted;
            dBTask.ModifiedAt = DateTime.Now;
            dBTask.Approved = toDotask.Approved;
            await _context.SaveChangesAsync();

            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpDelete]

        public async Task<ActionResult<List<Models.Task>>> DeleteTask(int id)
        {
            var dBTask = await _context.Tasks.FindAsync(id);
            if (dBTask == null)
            {
                return NotFound("Task not found");
            }
            _context.Tasks.Remove(dBTask);
            await _context.SaveChangesAsync();

            return Ok(await _context.Tasks.ToListAsync());
        }
    }
}

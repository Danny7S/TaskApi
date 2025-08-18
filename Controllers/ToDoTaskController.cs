using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskApi.DTO;
using TaskApi.Models;
using TaskApi.Services;
namespace TaskApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskService _service;

        public ToDoTaskController(IToDoTaskService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Models.TaskEntity>>> GetAllTasks()
        {

            var tasks = await _service.GetAllTasks();
            return Ok(tasks);
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<ActionResult<TaskEntityDTO>> GetByIdTask(int id)
        {
            var task = await _service.GetByIdTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<CreateTaskDTO>> CreateTask([FromBody] CreateTaskDTO createTask)
        {
            if (createTask == null)
            {
                return BadRequest("Task cannot be null");
            }
            var createdTask = await _service.CreateTask(createTask);
            return Ok(createdTask);
        }


        [Authorize(Roles = "User,Admin")]
        [HttpPut]
        public async Task<ActionResult<UpdateTaskDTO>> UpdateTask([FromBody] UpdateTaskDTO task)
        {
            if (task == null)
                return BadRequest("Task cannot be null");

            try
            {
                var updatedTask = await _service.UpdateTask(task);
                return Ok(updatedTask);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(Roles ="User,Admin")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteTask(int id)
        {
            var task=await _service.GetByIdTask(id);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            await _service.DeleteTaskById(id);
            return Ok();
        }
    }
}


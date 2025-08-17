
using Microsoft.EntityFrameworkCore;
using TaskApi.DTO;
using TaskApi.Models;

namespace TaskApi.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly TaskDbContext _context;
        public ToDoTaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntityDTO>> GetAllTasks()
        {
            var tempData = await _context.Tasks.ToListAsync();
            //if (!tempData.Any())
            //{
            //    throw new InvalidOperationException("No Tasks Found!");
            //}

            return tempData.Select(task => new TaskEntityDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                ModifiedAt = task.ModifiedAt
            }).ToList();
        }

        public async Task<TaskEntityDTO> GetByIdTask(int id)
        {
            var tempData = await _context.Tasks.FindAsync(id);
            if (tempData is null)
            {
                throw new InvalidOperationException("No task with the given id found!");
            }
            var result = new TaskEntityDTO
            {
                Id = id,
                Title = tempData.Title,
                Description = tempData.Description,
                IsCompleted = tempData.IsCompleted,
                ModifiedAt = tempData.ModifiedAt
            };
            return result;
        }

        public async Task<CreateTaskDTO> CreateTask(CreateTaskDTO task)
        {
            //if (task == null)
            //{
            //    throw new ArgumentNullException(nameof(task), "The task data cannot be null.");
            //}

            var taskEntity = new TaskEntity
            {
                Title = task.Title,
                Description = task.Description,
                ModifiedAt = task.ModifiedAt
            };

            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();
            return task;

        }

        public async Task<UpdateTaskDTO> UpdateTask(UpdateTaskDTO task)
        {

            var tempData = await _context.Tasks.FindAsync(task.Id);


            tempData.Title = task.Title;
            tempData.Description = task.Description;
            tempData.IsCompleted = task.IsCompleted;
            tempData.ModifiedAt = task.ModifiedAt;
            _context.Tasks.Update(tempData);
            await _context.SaveChangesAsync();

            return new UpdateTaskDTO
            { 
                Id=task.Id,
                Title=task.Title,
                Description=task.Description,
                IsCompleted=task.IsCompleted,
                ModifiedAt=task.ModifiedAt,
            };
        }

        public async Task<TaskEntityDTO> DeleteTaskById(int id)
        {
            var tempData = await _context.Tasks.FindAsync(id);
            var task = new TaskEntityDTO
            {
                Id = tempData.Id,
                Title = tempData.Title,
                Description = tempData.Description,
                IsCompleted = tempData.IsCompleted,
                ModifiedAt = tempData.ModifiedAt,
            };

            _context.Tasks.Remove(tempData);
            await _context.SaveChangesAsync();


            return (task);
        }

    }
}

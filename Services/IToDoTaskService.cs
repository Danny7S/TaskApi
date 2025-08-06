using TaskApi.DTO;

namespace TaskApi.Services
{
    public interface IToDoTaskService
    {
        Task<IEnumerable<TaskEntityDTO>> GetAllTasks();
        Task<TaskEntityDTO> GetByIdTask(int id);
        Task<CreateTaskDTO> CreateTask(CreateTaskDTO task);
        Task<UpdateTaskDTO> UpdateTask(UpdateTaskDTO toDoTask);
        Task<TaskEntityDTO> DeleteTaskById(int id);
    }
}

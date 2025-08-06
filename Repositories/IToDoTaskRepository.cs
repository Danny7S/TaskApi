using TaskApi.DTO;

namespace TaskApi.Repositories
{
    public interface IToDoTaskRepository
    {
        Task<IEnumerable<TaskEntityDTO>> GetAllTasks();
        Task<TaskEntityDTO> GetByIdTask(int id);
        Task<CreateTaskDTO> CreateTask(CreateTaskDTO task);
        Task<UpdateTaskDTO> UpdateTask(UpdateTaskDTO toDoTask);
        Task<TaskEntityDTO> DeleteTaskById(int id);
    }
}

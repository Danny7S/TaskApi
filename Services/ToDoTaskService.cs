using TaskApi.DTO;
using TaskApi.Repositories;

namespace TaskApi.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _taskRepository;

        public ToDoTaskService(IToDoTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskEntityDTO>> GetAllTasks()
        { 
            var tempData=await _taskRepository.GetAllTasks();

            if (!tempData.Any())
            {
                throw new InvalidOperationException("No Tasks Found!");
            }
            return await _taskRepository.GetAllTasks();
        }

        public async Task<TaskEntityDTO> GetByIdTask(int id)
        {
            var tempData=await _taskRepository.GetByIdTask(id);

            if (tempData is null)
            {
                throw new InvalidOperationException("No task with the given id found!");
            }
            return await _taskRepository.GetByIdTask(id);
        }

        public async Task<CreateTaskDTO> CreateTask(CreateTaskDTO task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "The task data cannot be null.");
            }

            return await _taskRepository.CreateTask(task);
        }

        public async Task<UpdateTaskDTO> UpdateTask(UpdateTaskDTO task)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task), "The input task data cannot be null.");
            }

            var tempData=await _taskRepository.UpdateTask(task);

            if (tempData is null)
            {
                throw new InvalidOperationException("No task with the given id found!");
            }
            return tempData;
        }

        public async Task<TaskEntityDTO> DeleteTaskById(int id)
        {
            var tempData= await _taskRepository.DeleteTaskById(id);
            if (tempData is null)
            {
                throw new InvalidOperationException("No task with the given id found!");
            }

            return tempData;
        }



    }
}

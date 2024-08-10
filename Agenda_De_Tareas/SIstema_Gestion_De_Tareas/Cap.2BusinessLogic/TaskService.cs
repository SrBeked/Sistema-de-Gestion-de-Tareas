using DataAccess;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class TaskService
    {
        private TaskRepository _taskRepository;

        public TaskService(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void CreateTask(string description)
        {
            _taskRepository.AddTask(description);
        }

        public List<(int Id, string Descripcion, bool Completada)> GetAllTasks()
        {
            return _taskRepository.GetTasks();
        }

        public void MarkTaskAsCompleted(int id)
        {
            _taskRepository.CompleteTask(id);
        }

        public void DeleteTask(int id)
        {
            _taskRepository.DeleteTask(id);
        }
    }
}

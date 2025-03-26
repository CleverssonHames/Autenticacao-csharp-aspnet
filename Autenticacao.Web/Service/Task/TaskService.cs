using Autenticacao.Web.Dto.Tarefa;
using Autenticacao.Web.Models;
using Autenticacao.Web.Models.ViewModel;
using Autenticacao.Web.Repositories.Task;

namespace Autenticacao.Web.Service.Task
{
    public class TaskService
    {
        private readonly TaskRepository _taskRepository;
        public TaskService(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public TarefasViewModel AddTask(AddTaskDto task)
        {
            var tasks = _taskRepository.ListTask(task.IdUsuario);

            var ret = _taskRepository.AddTask(task);

            ret.Tarefas = tasks.Tarefas;

            return ret;
        }
        public RetornoPadrao CheckTask(string id)
        {
            return _taskRepository.CheckTask(id);
        }
        
        public RetornoPadrao DeleteTask(string id)
        {
           return _taskRepository.DeleteTask(id);
        }

        public TarefasViewModel  ListTask(string idUsuario)
        {
           return _taskRepository.ListTask(idUsuario);
        }
    }
}

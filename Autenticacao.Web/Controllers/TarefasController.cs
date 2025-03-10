using Autenticacao.Web.Service.Task;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Web.Controllers
{
    public class TarefasController : Controller
    {
        private TerefaServices _taskService = new TerefaServices();
        public IActionResult Index(string idUsuario)
        {
            var model = _taskService.ListarTarefas(idUsuario);

            return View(model);
        }

        public IActionResult AddTask()
        {
            return View();
        }

        public IActionResult UpdateTask()
        {
            return View();
        }

        public IActionResult DeleteTask()
        {
            return View();
        }   
    }
}

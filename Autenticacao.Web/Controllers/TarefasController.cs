using Autenticacao.Web.Dto.Tarefa;
using Autenticacao.Web.Repositories.Task;
using Autenticacao.Web.Service.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Web.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        private readonly TaskService _taskService;
        public TarefasController(TaskService taskService)
        {
            _taskService = taskService;
        }
        public IActionResult Index(string idUsuario)
        {
            var model = _taskService.ListTask(idUsuario);

            return View(model);
        }
        [HttpPost]
        public IActionResult AddTask(AddTaskDto request)
        {
            
            if (ModelState.IsValid == false) 
            {
                var model = _taskService.ListTask(request.IdUsuario);
                return View("Index", model);
            }

            var ret = _taskService.AddTask(request);

            return RedirectToAction("Index", ret);
        }
        public IActionResult CheckTask(string id)
        {
            var result = _taskService.CheckTask(id);

            return Json(result);
        }
        [HttpPost]
        public IActionResult DeleteTask(string id)
        {
            var result = _taskService.DeleteTask(id);

            return Json(result);
        }   
    }
}

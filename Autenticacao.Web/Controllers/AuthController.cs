using Autenticacao.Web.Dto.Auth;
using Autenticacao.Web.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _authService.Login(email, senha);

            if (usuario.status == false)
            {
                ViewBag.Erro = usuario.mensagem;
                return View();
            }
    
            return RedirectToAction("Index", "Tarefas", new { idUsuario = usuario.id });
        }
        public IActionResult Logout()
        {
            _authService.Logout();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(RegistroUsuarioDto user)
        {
            if (ModelState.IsValid == false)
                return View("Register", user);

            var result = _authService.Register(user);

            if (result.status == false)
            {
                ViewBag.Erro = result.mensagem;
                return View("Register", user);
            }
              
            return RedirectToAction("Login", "Auth");
        }
    }
}

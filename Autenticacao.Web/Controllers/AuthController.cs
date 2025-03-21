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
        public IActionResult Login(string email, string senha)
        {
            var usuario = _authService.Login(email, senha);

            if (usuario.nome.Length > 0)
            {
                return RedirectToAction("Index", "Tarefas", new {idUsuario = usuario.id});
            }   
            return View();
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
            if (ModelState.IsValid)
            {
                _authService.Register(user);
                return RedirectToAction("Login", "Auth");
            }

            return View("Register", user);
        }
    }
}

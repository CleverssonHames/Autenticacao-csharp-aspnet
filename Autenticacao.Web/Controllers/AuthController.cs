using Autenticacao.Web.Dto.Auth;
using Autenticacao.Web.Models.Auth;
using Autenticacao.Web.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao.Web.Controllers
{
    public class AuthController : Controller
    {
        private AuthService _authService = new AuthService();
        public IActionResult Login(string email, string senha)
        {
            var usuario = _authService.Login(email, senha);

            if (usuario.nome.Length > 0)
            {
                return RedirectToAction("Index", "Tarefas");
            }   
            return View();
        }

        public IActionResult Logout()
        {
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

using Autenticacao.Web.Dto.Auth;
using Autenticacao.Web.Models;
using Autenticacao.Web.Models.Auth;
using Autenticacao.Web.Repositories.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Autenticacao.Web.Service.Auth
{
    public class AuthService
    {
        private readonly AuthRepository _authRepositorie;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(AuthRepository authRepository, IHttpContextAccessor httpContextAccessor)
        {
            _authRepositorie = authRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public UsuarioLogadoDto Login(string email, string senha)
        {

            var user = _authRepositorie.Login(email, senha);

            if (user.status == false)
                return user;

            if (string.IsNullOrWhiteSpace(user.id))
            {
                user.status = false;
                user.mensagem = "Usuário ou senha inválido";
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.nome),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return user;
        }
        public RetornoPadrao Register(RegistroUsuarioDto user)
        {
           return _authRepositorie.Register(user);
        }
        public void Logout()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

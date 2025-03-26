using Autenticacao.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Autenticacao.Web.Dto.Auth
{
    public class UsuarioLogadoDto : RetornoPadrao
    {
        public string id { get; set; } = String.Empty;
        public string nome { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
    }
}

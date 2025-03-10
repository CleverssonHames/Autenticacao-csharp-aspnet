using System.ComponentModel.DataAnnotations;

namespace Autenticacao.Web.Models.Auth
{
    public class Usuario
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string nome { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        public byte [] senha { get; set; } = new byte[0];
        public byte [] ConfimaSenha { get; set; } = new byte[0];
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
    }
}

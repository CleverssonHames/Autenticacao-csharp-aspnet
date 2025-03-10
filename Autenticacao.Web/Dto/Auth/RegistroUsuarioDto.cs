using System.ComponentModel.DataAnnotations;

namespace Autenticacao.Web.Dto.Auth
{
    public class RegistroUsuarioDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string nome { get; set; } = String.Empty;
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo email é inválido")]
        public string email { get; set; } = String.Empty;
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [MinLength(8, ErrorMessage = "O campo senha deve ter no mínimo 8 caracteres")]
        public string senha { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo senha é obrigatório"),
            Compare("senha", ErrorMessage = "As senhas devem ser iguais.")]
        public string confirmaSenha { get; set; } = string.Empty;
    }
}

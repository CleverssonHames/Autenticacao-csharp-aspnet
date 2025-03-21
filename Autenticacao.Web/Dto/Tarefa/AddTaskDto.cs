using System.ComponentModel.DataAnnotations;

namespace Autenticacao.Web.Dto.Tarefa
{
    public class AddTaskDto
    {
        public string IdUsuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo título é obrigatório")]
        [MinLength(3, ErrorMessage = "O campo descricao deve ter no mínimo 3 caracteres")]
        public string Descricao { get; set; } = string.Empty;
    }
}

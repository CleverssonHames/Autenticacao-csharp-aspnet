using Autenticacao.Web.Models.Tarefas;

namespace Autenticacao.Web.Models.ViewModel
{
    public class TarefasViewModel : RetornoPadrao
    {
        public string IdUsuario { get; set; } = string.Empty;
        public List<Tarefa> Tarefas { get; set; } = [];

    }
}

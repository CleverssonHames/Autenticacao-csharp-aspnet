namespace Autenticacao.Web.Models.Tarefas
{
    public class Tarefa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string IdUsuario { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Concluida { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }    
    }
}

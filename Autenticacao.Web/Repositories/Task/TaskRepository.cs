using Autenticacao.Web.Dto.Tarefa;
using Autenticacao.Web.Models;
using Autenticacao.Web.Models.Tarefas;
using Autenticacao.Web.Models.ViewModel;
using Autenticacao.Web.Repositories.Db;
using MySql.Data.MySqlClient;

namespace Autenticacao.Web.Repositories.Task
{
    public class TaskRepository
    {
        private readonly Database _db;
        public TaskRepository(Database db)
        {
            _db = db;
        }
        public TarefasViewModel AddTask(AddTaskDto tarefa)
        {
            var ret = new TarefasViewModel();
            var novaTarefa = new Tarefa
            {
                IdUsuario = tarefa.IdUsuario,
                Descricao = tarefa.Descricao,
            };

            var sql = "INSERT INTO Tarefas (Id, IdUsuario,Descricao, Concluida, DataCriacao) VALUES (@Id, @IdUsuario, @Descricao, @Concluida, @DataCriacao)";

            using (MySqlConnection con = _db.GetConnection())
            {
                con.Open();
                try
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", novaTarefa.Id);
                    cmd.Parameters.AddWithValue("@IdUsuario", novaTarefa.IdUsuario);
                    cmd.Parameters.AddWithValue("@Descricao", novaTarefa.Descricao);
                    cmd.Parameters.AddWithValue("@Concluida", novaTarefa.Concluida);
                    cmd.Parameters.AddWithValue("@DataCriacao", novaTarefa.DataCriacao);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ret.status = false;
                    ret.mensagem = $"Ocorreu um erro ao adicionar a tarefa!\n{ex.Message}";
                }
                con.Close();
            }

            ret.IdUsuario = tarefa.IdUsuario;
            ret.mensagem = "Tarefa adicionada com sucesso!";

            return ret;
        }
        public RetornoPadrao CheckTask(string id)
        {
            var ret = new RetornoPadrao();

            string sql = "update tarefas set Concluida = @Concluida where Id = @Id";

            using (MySqlConnection con = _db.GetConnection())
            {
                con.Open();
                try
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Concluida", 1);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ret.status = false;
                    ret.mensagem = $"Ocorreu um erro ao concluir a tarefa!\n{ex.Message}";
                }
                con.Close();
            }
            return ret;
        }
        public RetornoPadrao DeleteTask(string id)
        {
            var ret = new RetornoPadrao();

            var sql = "DELETE FROM Tarefas WHERE Id = @Id";

            using (MySqlConnection con = _db.GetConnection())
            {
                con.Open();
                try
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ret.status = false;
                    ret.mensagem = $"Ocorreu um erro ao deletar a tarefa!\n{ex.Message}";
                }
                con.Close();
            }

            ret.mensagem = "Tarefa deletada com sucesso!";

            return ret;
        }
        public TarefasViewModel ListTask(string idUsuario)
        {
            var ret = new TarefasViewModel();
            var sql = "SELECT Id, Descricao, Concluida, DataCriacao FROM tarefas WHERE IdUsuario = @IdUsuario order by DataCriacao desc";

            using (MySqlConnection con = _db.GetConnection())
            {
                con.Open();
                try
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var tarefa = new Tarefa
                            {
                                Id = Guid.Parse(Convert.ToString(dr["Id"])),
                                Descricao = Convert.ToString(dr["Descricao"]),
                                Concluida = Convert.ToBoolean(dr["Concluida"]),
                                DataCriacao = Convert.ToDateTime(dr["DataCriacao"]),
                            };
                            ret.Tarefas.Add(tarefa);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                con.Close();
            }

            ret.IdUsuario = idUsuario;
            ret.mensagem = "Tarefas listadas com sucesso!";

            return ret;
        }
    }
}

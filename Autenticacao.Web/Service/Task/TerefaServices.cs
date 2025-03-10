using Autenticacao.Web.Db;
using Autenticacao.Web.Dto.Tarefa;
using Autenticacao.Web.Models.Tarefas;
using MySql.Data.MySqlClient;

namespace Autenticacao.Web.Service.Task
{
    public class TerefaServices
    {
        private Database _db = new Database();
        public string AdicionarTarefa(AddTarefaDto tarefa)
        {
            var novaTarefa = new Tarefa
            {
                Descricao = tarefa.Descricao,
                Concluida = false
            };

            var sql = "INSERT INTO Tarefas (Id, IdUsuario, Descricao, Concluida, DataCriacao) VALUES (@Id, @IdUsuario, @Descricao, @Concluida, @DataCriacao)";

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
                    throw new Exception(ex.Message);
                }
                con.Close();
            }

            var result = "Tarefa adicionada com sucesso!";

            return result;
        }
        public void AtualizarTarefa(Tarefa tarefa)
        {
            // Atualiza tarefa
        }
        public string DeletarTarefa(Guid id)
        {
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
                    throw new Exception(ex.Message);
                }
                con.Close();
            }

            var result = "Tarefa deletada com sucesso!";

            return result;
        }
        public List<Tarefa> ListarTarefas(string idUsuario)
        {
            var sql = "SELECT * FROM Tarefas WHERE IdUsuario = @IdUsuario";

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
                        var tarefas = new List<Tarefa>();
                        while (dr.Read())
                        {
                            var tarefa = new Tarefa
                            {
                                Id = dr.GetGuid("Id"),
                                IdUsuario = dr.GetString("IdUsuario"),
                                Descricao = dr.GetString("Descricao"),
                                Concluida = dr.GetBoolean("Concluida"),
                                DataCriacao = dr.GetDateTime("DataCriacao"),
                                DataAlteracao = dr.GetDateTime("DataAlteracao")
                            };
                            tarefas.Add(tarefa);
                        }
                        return tarefas;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                con.Close();
            }

            return new List<Tarefa>();
        }
    }
}

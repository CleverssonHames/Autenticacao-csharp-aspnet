using Autenticacao.Web.Db;
using Autenticacao.Web.Dto.Auth;
using Autenticacao.Web.Models.Auth;
using MySql.Data.MySqlClient;
using System.Text;

namespace Autenticacao.Web.Service.Auth
{
    public class AuthService
    {
        private Database _db = new Database();
        public Usuario Login(string email, string senha)
        {
            var usuario = new Usuario();
            var sql = "SELECT * FROM usuario WHERE email = @email AND senha = @senha";

            using (MySqlConnection con = _db.GetConnection())
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = con.CreateCommand();

                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario.id = dr.GetGuid("idusuario");
                            usuario.nome = dr.GetString("nome");
                            usuario.email = dr.GetString("email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                con.Close();
            }
            return usuario;
        }

        public void Register(RegistroUsuarioDto user)
        {
            var newUser = new Usuario
            {
                nome = user.nome,
                email = user.email,
                senha = Encoding.UTF8.GetBytes(user.senha)
            };

            var sql = "INSERT INTO usuario (idusuario, nome, email, senha, datacadastro) VALUES (@id, @nome, @email, @senha, @datacadastro)";

            using (MySqlConnection con = _db.GetConnection())
            {
                con.Open();
                try
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", newUser.id);
                    cmd.Parameters.AddWithValue("@nome", newUser.nome);
                    cmd.Parameters.AddWithValue("@email", newUser.email);
                    cmd.Parameters.AddWithValue("@senha", newUser.senha);
                    cmd.Parameters.AddWithValue("@datacadastro", newUser.DataCadastro);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                con.Close();
            }
        }
    }
}

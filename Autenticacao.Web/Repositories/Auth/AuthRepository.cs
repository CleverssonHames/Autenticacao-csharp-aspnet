using Autenticacao.Web.Dto.Auth;
using Autenticacao.Web.Models;
using Autenticacao.Web.Models.Auth;
using Autenticacao.Web.Repositories.Db;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System.Text;

namespace Autenticacao.Web.Repositories.Auth
{
    public class AuthRepository
    {
        private readonly Database _db;
        public AuthRepository(Database db)
        {
            _db = db;
        }
        public UsuarioLogadoDto Login(string email, string senha)
        {
            var usuario = new UsuarioLogadoDto();
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
                            usuario.id = Convert.ToString(dr.GetGuid("idusuario"));
                            usuario.nome = dr.GetString("nome");
                            usuario.email = dr.GetString("email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    usuario.status = false;
                    usuario.mensagem = $"Ocorreu um erro ao realizar o login: {ex.Message}";
                }
                con.Close();
            }
            return usuario;
        }
        public RetornoPadrao Register(RegistroUsuarioDto user)
        {
            var ret = new RetornoPadrao();

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
                    ret.status = false;
                    ret.mensagem = $"Ocorreu um erro ao realizar a inclusão: {ex.Message}";
                }
                con.Close();
            }

            return ret;
        }
    }
}

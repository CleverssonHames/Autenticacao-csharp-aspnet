using MySql.Data.MySqlClient;

namespace Autenticacao.Web.Repositories.Db
{
    public class Database
    {
        private static string _connectionString = "Server=localhost;Database=autenticacaodb;User Id=root;Password=@Hames2015";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}

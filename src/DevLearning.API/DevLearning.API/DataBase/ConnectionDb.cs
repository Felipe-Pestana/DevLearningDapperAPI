using Microsoft.Data.SqlClient;

namespace DevLearning.API.DataBase
{
    public class ConnectionDb
    {
        private readonly string _connectionString;

        public ConnectionDb(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

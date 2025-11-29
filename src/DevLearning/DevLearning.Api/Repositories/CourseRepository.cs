using Dapper;
using DevLearning.Api.Data;
using DevLearning.Api.Models;
using Microsoft.Data.SqlClient;
using System.Net.WebSockets;

namespace DevLearning.Api.Repositories
{
    public class CourseRepository
    {
        private readonly SqlConnection _connection;
        public CourseRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        
    }
}

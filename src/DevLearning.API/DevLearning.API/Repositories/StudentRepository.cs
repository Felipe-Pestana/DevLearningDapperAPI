using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SqlConnection _connection;
        public StudentRepository(ConnectionDB dbConnection)
        {
            _connection = dbConnection.GetConnection();
        }
        public Task CreateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}

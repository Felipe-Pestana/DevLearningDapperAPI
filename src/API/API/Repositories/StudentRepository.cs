using API.Database;
using API.Models;
using API.Models.DTOs.Student;
using API.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SqlConnection _connection;

        public StudentRepository(DbConnectionFactory dbConnection)
        {
            _connection = dbConnection.GetConnection();
        }

        public async Task CreateStudentAsync(Student student)
        {
            var sql = @"INSERT INTO Student (Id, Name, Email, Document, Phone, Birthdate, CreateDate)
                        VALUES (@Id, @Name, @Email, @Document, @Phone, @Birthdate, @CreateDate)";

            await _connection.ExecuteAsync(sql, new
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Document = student.Document,
                Phone = student.Phone,
                Birthdate = student.Birthdate.ToDateTime(new TimeOnly(0, 0)),
                CreateDate = student.CreateDate
            });
        }

        public async Task<int> DeleteStudentAsync(Guid id)
        {
            var sql = "DELETE FROM Student WHERE Id = @Id";
            var rowsAffected =  await _connection.ExecuteAsync(sql, new { Id = id });

            if (rowsAffected > 0)
            {
                sql = "DELETE FROM StudentCourse WHERE StudentId = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            }

            return rowsAffected;
        }

        public async Task<List<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var sql = @"SELECT Id, Name, Email, Phone, Birthdate FROM Student ORDER BY CreateDate";

            return (await _connection.QueryAsync<StudentResponseDTO>(sql)).ToList();
        }

        public async Task<StudentUpdateDTO?> SearchStudentToUpdateAsync(Guid id)
        {
            var sql = @"SELECT Name, Email, Phone FROM Student WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<StudentUpdateDTO>(sql, new { Id = id });
        }

        public async Task UpdateStudentAsync(Guid id, StudentUpdateDTO student)
        {
            var sql = @"UPDATE Student SET
                            Name = @Name,
                            Email = @Email,
                            Phone = @Phone
                        WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new
            {
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                Id = id
            });
        }
    }
}

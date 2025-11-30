using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Student;
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
        public async Task CreateStudent(Student student)
        {
            try
            {
                var sql = @"INSERT INTO Student VALUES (@Id, @Name, @Email, @Document, @Phone, @Birthdate, @CreateDate)";
                await _connection.ExecuteAsync(sql, new { Id = student.Id, Name = student.Name, Email = student.Email, Document = student.Document, Phone = student.Phone, Birthdate = student.Birthdate, CreateDate = student.CreateDate });
                
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteStudent(Guid id)
        {
           try
            {
                var sql = @"DELETE FROM Student WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<StudentResponseDTO>> GetAllStudents()
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate FROM Student";
                var students = (await _connection.QueryAsync<StudentResponseDTO>(sql)).ToList();
                return students;
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentByDocument(string document)
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate FROM Student WHERE Document = @Document";
                var student = await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { Document = document });
                return student;
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentByEmail(string email)
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate FROM Student WHERE Email = @Email";
                var student = await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { Email = email });
                return student;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Student> GetStudentById(Guid id)
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate FROM Student WHERE Id = @Id";
                var student = await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { Id = id });
                return student;
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateStudent(Student student, Guid id)
        {
            try
            {
                var sql = @"UPDATE FROM Student SET
                            Name = @Name,
                            Email = @Email,
                            Document = @Document,
                            Phone = @Phone,
                            Birthdate = @Birthdate
                            WHERE Id = @id";
                await _connection.ExecuteAsync(sql, new { Name = student.Name, Email = student.Email, Document = student.Document, Phone = student.Phone, Birthdate = student.Birthdate });
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

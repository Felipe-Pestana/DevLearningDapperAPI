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

        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate";
                var students = (await _connection.QueryAsync<Student>(sql)).ToList();
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
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate WHERE Document = @Document";
                var student = (await _connection.QueryFirstOrDefaultAsync<Student>(sql));
                return student;
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateStudent(StudentRequestDTO student)
        {
            //try
            //{

            //} catch(SqlException ex)
            //{
            //    throw new Exception(ex.Message);
            //} catch(Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            throw new NotImplementedException();
        }
    }
}

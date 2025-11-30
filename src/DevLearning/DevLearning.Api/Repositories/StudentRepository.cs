using Dapper;
using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Student;
using DevLearning.Api.Models.Dtos.StudentCourse;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DevLearning.Api.Repositories
{
    public class StudentRepository
    {
        private readonly SqlConnection _connection;
        public StudentRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task CreateStudentAsync(Student student) 
        {
            try
            {
                var sql = @"INSERT INTO Student (Id, Name, Email, Document, Phone, BirthDate, CreateDate)
                            VALUES (@Id, @Name, @Email, @Document, @Phone, @BirthDate, @CreateDate)";

                await _connection.ExecuteAsync(sql, new 
                {   student.Id,
                    student.Name, 
                    student.Email, 
                    Document = student.Document == null ? (object)DBNull.Value : student.Document,
                    Phone = student.Phone == null ? (object)DBNull.Value : student.Phone, 
                    BirthDate = student.BirthDate == null ? (object)DBNull.Value : student.BirthDate, 
                    student.CreateDate 
                });
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace); 
            }
        }

        public async Task<List<StudentResponseDto>> GetAllStudentsAsync() 
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, BirthDate, CreateDate 
                            FROM Student";

                return (await _connection.QueryAsync<StudentResponseDto>(sql)).ToList();

            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            } 
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, BirthDate, CreateDate 
                            FROM Student
                            WHERE Id = @Id";

                return await _connection.QueryFirstOrDefaultAsync<StudentResponseDto>(sql, new {@Id = id});

            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task UpdateStudentAsync(Guid id, UpdateStudentDto student) 
        {
            try
            {
                var sql = @"UPDATE Student 
                            SET Email = @Email,
                            Document = @Document,
                            Phone = @Phone
                            WHERE Id = @Id";

                await _connection.ExecuteAsync(sql, new 
                    {student.Email,
                    Document = student.Document == null ? (object)DBNull.Value : student.Document,
                    Phone = student.Phone == null ? (object)DBNull.Value : student.Phone,
                    @Id = id });
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            try
            {
                var sql = @"DELETE FROM Student
                            WHERE Id = @Id";

                await _connection.ExecuteAsync(sql, new { @Id = id });
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task CreateStudentCourseAsync(StudentCourse studentCourse) 
        {
            try
            {
                var sql = @"INSERT INTO StudentCourse(CourseId, StudentId, Progress, Favorite, StartDate, LastUpdateDate) 
                        VALUES(@CourseId, @StudentId, @Progress, @Favorite, @StartDate, @LastUpdateDate)";

                await _connection.ExecuteAsync(sql, new
                {
                    studentCourse.CourseId,
                    studentCourse.StudentId,
                    studentCourse.Progress,
                    studentCourse.Favorite,
                    studentCourse.StartDate,
                    studentCourse.LastUpdateDate
                });
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task <StudentCourse?> GetStudentCourseAsync(Guid courseId, Guid studentId) 
        {
            try
            {
                var sql = @"SELECT CourseId, StudentId, Progress, Favorite, StartDate, LastUpdateDate
                        FROM StudentCourse 
                        WHERE CourseId = @courseId
                        AND StudentId = @StudentId";

                return await _connection.QueryFirstOrDefaultAsync(sql, new
                { @CourseId = courseId, @StudentId = studentId });
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }

        }

        public async Task UpdateStudentCourseProgressAsync(Guid studentId, Guid courseId, UpdateStudentCourseDto student)
        {
            try
            {
                var sql = @"UPDATE StudentCourse 
                            SET Progress = @Progress
                            WHERE CourseId = @courseId
                            AND StudentId = @StudentId";

                await _connection.ExecuteAsync(sql, new
                {
                    student.Progress,
                    @CourseId = courseId,
                    @StudentId = studentId
                });
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }
        








        //🔹 Listar cursos de um aluno

        // GET /students/{id
        //     }/courses


        //public async Task<IEnumerable<StudentResponseDto>> GetStudentCoursesAsync(Guid id) 
        //{

        //}









    }
}

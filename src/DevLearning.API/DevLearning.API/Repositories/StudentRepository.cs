using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Course;
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

        public async Task DeleteStudent(Guid id)
        {
            try
            {
                var sqlDeleteStudentCourse = @"DELETE FROM StudentCourse WHERE StudentId = @StudentId";
                await _connection.ExecuteAsync(sqlDeleteStudentCourse, new { Id = id });

                var sql = @"DELETE FROM Student WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
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

        public async Task<List<StudentResponseDTO>> GetAllStudents()
        {
            try
            {
                var sql = @"SELECT Id, Name, Email, Document, Phone, Birthdate, CreateDate FROM Student";
                var students = (await _connection.QueryAsync<StudentResponseDTO>(sql)).ToList();
                return students;
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

        public async Task<List<StudentWithCourseResponseDTO>> GetAllStudentsWithCourses()
        {
            try
            {
                var sql = @"SELECT s.Id as StudentId, Name, Email, Document, Phone, Birthdate, CreateDate,
                            c.Id as CourseId, c.Title as Title, c.Summary as Summary, c.[Url] as Url, c.[Level] as Level, c.DurationInMinutes
                            FROM Student s
                            INNER JOIN StudentCourse sc
                            ON sc.StudentId = s.Id
                            INNER JOIN Course c
                            ON sc.CourseId = c.Id";

                var students = await _connection.QueryAsync<StudentWithCourseResponseDTO, CourseStudentDTO, StudentWithCourseResponseDTO>(sql, (student, course) =>
                {
                    student.Courses.Add(course);
                    return student;
                }, splitOn: "CourseId");

                var result = students.GroupBy(s => s.Id).Select(g =>
                {
                    var groupedStudent = g.First();
                    groupedStudent.Courses = g.Select(s => s.Courses.Single()).ToList();
                    return groupedStudent;
                });

                return result.ToList();
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

        public async Task<StudentWithCourseResponseDTO> GetStudentWithCoursesById(Guid studentId)
        {
            try
            {
                var sql = @"SELECT s.Id as StudentId, Name, Email, Document, Phone, Birthdate, CreateDate,
                            c.Id as CourseId, c.Title as Title, c.Summary as Summary, c.[Url] as Url, c.[Level] as Level, c.DurationInMinutes
                            FROM Student s
                            INNER JOIN StudentCourse sc
                            ON sc.StudentId = s.Id
                            INNER JOIN Course c
                            ON sc.CourseId = c.Id WHERE s.Id = @Id";

                var student = await _connection.QueryAsync<StudentWithCourseResponseDTO, CourseStudentDTO, StudentWithCourseResponseDTO>(sql, (student, course) =>
                {
                    student.Courses.Add(course);
                    return student;
                }, new { Id = studentId }, splitOn: "CourseId");

                var result = student.GroupBy(s => s.Id).Select(g =>
                {
                    var groupedStudent = g.First();
                    groupedStudent.Courses = g.Select(s => s.Courses.Single()).ToList();
                    return groupedStudent;
                });

                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertStudentCourse(Guid studentId, Guid courseId, StudentRequestInsertCourseDTO studentCourse)
        {
            try
            {
                var sql = @"INSERT INTO StudentCourse (CourseId, StudentId, Favorite, Progress, StartDate) VALUES (@CourseId, @StudentId, @Favorite, @Progress, @StartDate)";
                await _connection.ExecuteAsync(sql, new { CourseId = courseId, StudentId = studentId, Favorite = studentCourse.Favorite, Progress = studentCourse.Progress, StartDate = studentCourse.StartDate });
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

        public async Task<StudentCourseResponseDTO> GetStudentCourse(Guid studentId, Guid courseId)
        {
            try
            {
                var sql = @"SELECT 
                            s.Id AS Id, 
                            s.Name AS [Name], 
                            s.Email AS Email, 
                            s.Document AS Document, 
                            s.Phone AS Phone, 
                            s.Birthdate AS BirthDate, 
                            s.CreateDate AS CreateDate,
                            c.Id AS CourseId, 
                            c.Title AS Title, 
                            c.Summary AS Summary, 
                            c.Url AS [Url], 
                            c.Level AS [Level], 
                            c.DurationInMinutes AS DurationInMinutes,
                            sc.Progress AS Progress, 
                            sc.Favorite AS Favorite, 
                            sc.StartDate AS StartDate, 
                            sc.LastUpdateDate AS LastUpdateDate
                        FROM StudentCourse sc
                        INNER JOIN Student s ON sc.StudentId = s.Id
                        INNER JOIN Course c ON sc.CourseId = c.Id
                        WHERE sc.StudentId = @StudentId
                          AND sc.CourseId = @CourseId;";
                var studentCourse = await _connection.QueryAsync<
                    StudentResponseDTO, 
                    CourseStudentDTO, 
                    StudentCourseResponseDTO, 
                    StudentCourseResponseDTO>(sql,  (student, course, studentCourse) =>
                {
                    studentCourse.Student = student;
                    studentCourse.Course = course;
                    return studentCourse;
                }, 
                param: new { StudentId = studentId, CourseId = courseId },
                splitOn: "CourseId, Progress"
                );

                return studentCourse.FirstOrDefault();
            } catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourseRequestUpdateDTO studentCourse)
        {
            try
            {
                var sql = @"UPDATE StudentCourse SET 
                            Progress = @Progress,
                            Favorite = @Favorite WHERE CourseId = @CourseId AND StudentId = @StudentId";
                await _connection.ExecuteAsync(sql, new { Progress = studentCourse.Progress, Favorite = studentCourse.Favorite, CourseId = courseId, StudentId = studentId });
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

        public async Task<Student> GetStudentByEmailAndDocument(string email, string document)
        {
            try
            {
                var sql = @"select  Id, Name, Email, Document, Phone, Birthdate, CreateDate from Student s
                            WHERE s.Email = @Email AND s.Document = @Document;";
                return await _connection.QueryFirstOrDefaultAsync<Student>(sql, new { Email = email, Document = document });
            } catch(SqlException ex )
            {
                throw new Exception(ex.Message);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

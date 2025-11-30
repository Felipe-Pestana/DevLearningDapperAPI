using Dapper;
using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Data.SqlClient;
using System.Net.WebSockets;

namespace DevLearning.Api.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SqlConnection _connection;
        public CourseRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }

        public async Task CreateCourseAsync(Course course)
        {
            var sql = @"INSERT INTO [Course] (
                          [Id], [Tag], [Title], [Summary], [Url], [Level], 
                          [DurationInMinutes], [CreateDate], [LastUpdateDate], 
                          [Active], [Free], [Featured], 
                          [AuthorId], [CategoryId], [Tags]
                          )
                        VALUES (
                          @Id, @Tag, @Title, @Summary, @Url, @Level, 
                          @DurationInMinutes, @CreateDate, @LastUpdateDate,
                          @Active, @Free, @Featured,
                          @AuthorId, @CategoryId, @Tags
                          )";

            try
            {
                await _connection.ExecuteAsync(
                    sql, new
                    {
                        course.Id, course.Tag, course.Title, 
                        course.Summary, course.Url, course.Level, 
                        course.DurationInMinutes, 
                        course.CreateDate, course.LastUpdateDate, 
                        course.Active, course.Free, course.Featured, 
                        course.AuthorId, course.CategoryId, course.Tags
                    }
                    );
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            var sql = @"SELECT [Title], [Summary], [Tag], [AuthorId], [CategoryId], 
                          [Url], [Level], [DurationInMinutes], [Active], [Free], 
                          [Featured], [Tags]
                        FROM [Course]";

            try
            {
                return (await _connection.QueryAsync<CourseResponseDto>(sql)).ToList();
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseResponseDto?> GetCourseByIdAsync(Guid id)
        {
            var sql = @"SELECT [Title], [Summary], [Tag], [AuthorId], [CategoryId], 
                          [Url], [Level], [DurationInMinutes], [Active], [Free], 
                          [Featured], [Tags]
                        FROM [Course]
                        WHERE Id = @Id";

            try
            {
                return await _connection.QuerySingleOrDefaultAsync<CourseResponseDto>(sql, new {Id = id});
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCourseAsync(Guid id, Course course)
        {
            var sql = @"UPDATE [Course] 
                        SET [Summary] = @Summary, [Active] = @Active, 
                          [Free] = @Free, [Featured] = @Featured, [Tags] = @Tags
                        WHERE Id = @Id";

            try
            {
                await _connection.ExecuteAsync(
                    sql, new
                    {
                        course.Summary,
                        course.Active,
                        course.Free,
                        course.Featured,
                        course.Tags,
                        Id = id
                    });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var sql = "DELETE FROM Course WHERE Id = @Id";

            try
            {
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteCourseByAuthorIdAsync(Guid id)
        {
            var sql = "DELETE FROM Course WHERE AuthorId = @AuthorId";

            try
            {
                return await _connection.ExecuteAsync(sql, new { AuthorId = id });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteCourseByCategoryIdAsync(Guid id)
        {
            var sql = "DELETE FROM Course WHERE CategoryId = @CategoryId";

            try
            {
                return await _connection.ExecuteAsync(sql, new { CategoryId = id });
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

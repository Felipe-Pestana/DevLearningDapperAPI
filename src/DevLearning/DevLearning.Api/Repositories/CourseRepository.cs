using Dapper;
using DevLearning.Api.Data;
using DevLearning.Api.Models;
using DevLearning.Api.Models.Dtos.Course;
using DevLearning.Api.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

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
        }

        public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            var sql = @"SELECT c.[Title], c.[Summary], c.[Tag], 
						  a.[Name] AS Author, 
						  ca.[Title] AS Category, 
                          c.[Url], c.[Level], c.[DurationInMinutes], c.[Active], c.[Free], 
                          c.[Featured], c.[Tags]
                        FROM [Course] c
                        JOIN [Author] a ON c.AuthorId = a.Id
						JOIN [Category] ca ON c.CategoryId = ca.Id;";

            try
            {
                return (await _connection.QueryAsync<CourseResponseDto>(sql)).ToList();
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
        }

        public async Task<CourseResponseDto?> GetCourseByIdAsync(Guid id)
        {
            var sql = @"SELECT c.[Title], c.[Summary], c.[Tag], 
						  a.[Name] AS Author, 
						  ca.[Title] AS Category, 
                          c.[Url], c.[Level], c.[DurationInMinutes], c.[Active], c.[Free], 
                          c.[Featured], c.[Tags]
                        FROM [Course] c
                        JOIN [Author] a ON c.AuthorId = a.Id
						JOIN [Category] ca ON c.CategoryId = ca.Id
                        WHERE c.Id = @Id";

            try
            {
                return await _connection.QuerySingleOrDefaultAsync<CourseResponseDto>(sql, new {Id = id});
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
        }

        public async Task<CourseDataDto?> GetCourseToUpdateAsync(Guid id)
        {
            var sql = @"SELECT [Tag], [Title], [Summary], [Url], 
                          [Level], [DurationInMinutes],
                          [CreateDate], [Active], [Free], 
                          [Featured], [AuthorId], [CategoryId], 
                          [Tags]
                        FROM [Course]
                        WHERE Id = @Id";

            try
            {
                return await _connection.QuerySingleOrDefaultAsync<CourseDataDto>(sql, new { Id = id});
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
        }

        public async Task<bool> GetCourseTitleAsync(string title)
        {
            var sql = "SELECT [Title] FROM [Course] WHERE [Title] = @Title";

            try
            {
                var courses = await _connection.QueryAsync(sql, new { Title = title});
                if (courses.Any())
                    return true;
                else
                    return false;
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
            }
        }

        public async Task<bool> GetCourseUrlAsync(string url)
        {
            var sql = "SELECT [Url] FROM [Course] WHERE [Url] = @Url";

            try
            {
                var courses = await _connection.QueryAsync(sql, new { Url = url });
                if (courses.Any())
                    return true;
                else
                    return false;
            }
            catch (SqlException sqlex)
            {
                throw new Exception(sqlex.Message);
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
        }
    }
}

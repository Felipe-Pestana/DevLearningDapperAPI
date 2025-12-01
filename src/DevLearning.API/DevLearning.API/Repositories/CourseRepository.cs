using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Azure;
using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Course;
using DevLearning.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace DevLearning.API.Repositories
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
            var sql = @"INSERT INTO Course(Id, Tag, Title, Summary, [Url], [Level], DurationInMinutes,
                      CreateDate, LastUpdateDate, Active, Free, Featured, AuthorId, CategoryId, Tags) 
                      VALUES(@Id, @Tag, @Title, @summary, @Url, @Level, @DurationInMinutes, @CreateDate, @LastUpdateDate,
                      @Active, @Free, @Featured, @AuthorId, @CategoryId, @Tags)";

            await _connection.ExecuteAsync(sql, new { course.Id, course.Tag, course.Title, course.Summary, course.Url, course.Level, course.DurationInMinutes, course.CreateDate, course.LastUpdateDate, course.Active, course.Free, course.Featured, course.AuthorId, course.CategoryId, course.Tags });
        }

        public async Task<CourseResponseDTO> DeleteCourseByIdAsync(string title)
        {
            var sql = @"DELETE FROM Course WHERE title = @title";

            return (await _connection.QueryFirstOrDefaultAsync<CourseResponseDTO>(sql, new { title = title }));
        }

        public async Task<List<CourseResponseDTO>> GetAllCoursesAsync()
        {
            var sql = @"SELECT c.Tag, c.Title, c.Summary, c.[Url], c.[Level], c.DurationInMinutes,
                       c.CreateDate, c.LastUpdateDate, c.Active, c.Free, c.Featured, a.[Name] AS authorName, 
                       ca.Title AS categoryName, c.Tags FROM Course c
                       JOIN Author a ON a.Id = c.AuthorId
                       JOIN Category ca ON ca.Id = c.CategoryId;";

            return (await _connection.QueryAsync<CourseResponseDTO>(sql)).ToList();
        }

        public async Task<CourseResponseDTO> GetOneCourseByTitleAsync(string title)
        {
            var sql = @"SELECT c.Tag, c.Title, c.Summary, c.[Url], c.[Level], c.DurationInMinutes,
                       c.CreateDate, c.LastUpdateDate, c.Active, c.Free, c.Featured, a.[Name] AS authorName, 
                       ca.Title AS categoryName, c.Tags FROM Course c
                       JOIN Author a ON a.Id = c.AuthorId
                       JOIN Category ca ON ca.Id = c.CategoryId WHERE c.title = @Titulo";

            return (await _connection.QueryFirstOrDefaultAsync<CourseResponseDTO>(sql, new { Titulo = title }));
        }

        public async Task<CourseResponseDTO> GetOneCourseByIdAsync(Guid id)
        {
            var sql = @"SELECT c.Tag, c.Title, c.Summary, c.[Url], c.[Level], c.DurationInMinutes,
                       c.CreateDate, c.LastUpdateDate, c.Active, c.Free, c.Featured, a.[Name] AS authorName, 
                       ca.Title AS categoryName, c.Tags FROM Course c
                       JOIN Author a ON a.Id = c.AuthorId
                       JOIN Category ca ON ca.Id = c.CategoryId WHERE c.Id = @Id";

            return (await _connection.QueryFirstOrDefaultAsync<CourseResponseDTO>(sql, new { Id = id }));
        }

        public async Task UpdateCourseAsync(string title, bool active, bool free, bool featured, DateTime lastUpdateDate)
        {
            var sql = @"UPDATE Course SET Active = @active, Free = @free, Featured = @featured, LastUpdateDate = @lastUpdateDate WHERE Title = @Title";

            await _connection.ExecuteAsync(sql, new {active = active, Free = free, Featured = featured, LastUpdateDate = lastUpdateDate, Title = title });
        }
    }
}

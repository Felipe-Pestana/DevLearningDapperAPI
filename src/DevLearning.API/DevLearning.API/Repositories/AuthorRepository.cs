using Dapper;
using DevLearning.API.DataBase;
using DevLearning.API.Models;
using DevLearning.API.Models.DTOs.Author;
using DevLearning.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.API.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public readonly SqlConnection _connection;

        public AuthorRepository(ConnectionDB connection)
        {
            _connection = connection.GetConnection();
        }
        public async Task<List<AuthorResponseDTO>> GetAllAuthorsAsync()
        {
            var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM [Author]";

            var authors = (await _connection.QueryAsync<AuthorResponseDTO>(sql)).ToList();

            return authors;
        }
        public async Task<AuthorResponseDTO> GetAuthorByIdAsync(Guid id)
        {
            var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM [Author] WHERE Id = @Id";
            var author = await _connection.QueryFirstOrDefaultAsync<AuthorResponseDTO>(sql, new { Id = id });
            return author;
        }

        public async Task<AuthorResponseDTO> GetAuthorByEmail(string email)
        {
            var sql = "SELECT Name, Title, Image, Bio, Url, Email, Type FROM [Author] WHERE Email = @Email";
            var author = await _connection.QueryFirstOrDefaultAsync<AuthorResponseDTO>(sql, new { Email = email });
            return author;
        }
        public async Task CreateAuthorAsync(Author author)
        {
            var sql = @"INSERT INTO [Author] (Id, Name, Title, Image, Bio, Url, Email, Type) 
                        VALUES (@Id, @Name, @Title, @Image, @Bio, @Url, @Email, @Type) ";

            await _connection.ExecuteAsync(sql, new { author.Id, author.Name, author.Title, author.Image, author.Bio, author.Url, author.Email, author.Type });
        }
        public async Task DeleteAuthorAsync(Guid id)
        {
            //deletar os cursos do autor antes de deletar o autor
            var sqlDeleteCourse = "DELETE FROM [Course] WHERE AuthorId = @Id";
            await _connection.ExecuteAsync(sqlDeleteCourse, new { Id = id });

            var sql = "DELETE FROM [Author] WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdateAuthorAsync(Guid id, UpdateAuthorDTO author)
        {
            var sql = @"UPDATE [Author] 
                        SET Name = @Name, Title = @Title, Image = @Image, Bio = @Bio, Url = @Url, Type = @Type
                        WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { author.Name, author.Title, author.Image, author.Bio, author.Url, author.Type, Id = id });
        }

        //public async Task GetAuthorsCourses()  ---- FAZER DEPOIS A LÓGICA PARA PEGAR OS CURSOS DE CADA AUTOR
        //{
        //    var sql = @"SELECT a.Name AS AuthorName, c.Title AS CourseTitle
        //                FROM Author a
        //                JOIN Course c ON a.Id = c.AuthorId";
        //    var authorCourses = await _connection.QueryAsync(sql);
        //    
        //}
    }
}
